using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;
using System.Linq;

public enum DefinedLayer
{
    PLAYER = 8,
    PLAYER_BULLET = 9,
    ENEMY = 10,
    ENEMY_BULLET = 11,
}

public static partial class EnumExtend
{
    public static string GetTypeName(this DefinedLayer param)
    {
        switch (param)
        {
            case DefinedLayer.PLAYER:
                return "A";
            case DefinedLayer.PLAYER_BULLET:
                return "A-Bullet";
            case DefinedLayer.ENEMY:
                return "B";
            case DefinedLayer.ENEMY_BULLET:
                return "B-Bullet";
            default:
                return null;
        }
    }
}

public class ApproachLinear : MonoBehaviour
{
    public enum State
    {
        IDLE,
        DETECT
    }

    [SerializeField]
    private Collider2D searhArea;
    [SerializeField]
    private float keepDistanceRadius = 4f;
    [SerializeField]
    private float speed = 4f;
    [SerializeField]
    private float rotateSpeed = 0.01f;
    [SerializeField]
    private DefinedLayer layer;
    private State state;

    // Start is called before the first frame update
    void Start()
    {
        var body = GetComponent<Rigidbody2D>();

        searhArea
            .OnTriggerStay2DAsObservable()
            .Where(c => c.gameObject.layer == (int)layer)
            .Select(c => c.transform)
            .Subscribe(target =>
            {
                state = State.DETECT;

                //Debug.Log($"[ApproachLinear] target:{target}, me:{searhArea}");
                var distance = target.position - transform.position;
                var velocity = Vector2.zero;
                var direction = transform.rotation * 
                Vector3.up * speed;

                if (distance.magnitude > keepDistanceRadius)
                {
                    //Debug.Log($"[Keep distance] {distance.magnitude} > {keepDistanceRadius}");
                    var r = Vector3.Cross(distance, direction).z;
                    //body.AddTorque(100f * );
                    velocity = Quaternion.Euler(0, 0, -rotateSpeed * r) * direction;//distance.normalized * speed;
                }

                body.velocity = velocity;

                //TODO：見えている場合だけにしたいときはこれを実装
                //if (Physics.Linecast(transform.position, target.position))
                //{
                //    Debug.Log("blocked");
                //}
            })
            .AddTo(this);

        searhArea
            .OnTriggerExit2DAsObservable()
            .Where(c => c.gameObject.layer == (int)layer)
            .Subscribe(_ =>
            {
                state = State.IDLE;
            });

        this.UpdateAsObservable()
            .Where(_ => state == State.IDLE)
            .Subscribe(_ =>
            {
                body.velocity = Vector2.zero;
            });
    }
}
