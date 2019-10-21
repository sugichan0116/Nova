using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;
using System.Linq;
using UnityEditorInternal;
using NaughtyAttributes;

public class TagManager
{
    public static readonly string PLAYER = "A:Player";
    public static readonly string ENEMY = "B:Enemy";
    public static readonly string NEUTRAL = "C:Neutral";
    public static string[] TAGS = new string[] { PLAYER, ENEMY, NEUTRAL };
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
    [Dropdown("tags")] string targetTag;
    private readonly string[] tags = TagManager.TAGS;
    private State state;

    // Start is called before the first frame update
    void Start()
    {
        var body = GetComponent<Rigidbody2D>();
        Transform target = null;

        searhArea
            .OnTriggerEnter2DAsObservable()
            .Where(c => IsTarget(c.gameObject))
            .Subscribe(c =>
            {
                Debug.Log($"[Move AI] detected! {this}");
                state = State.DETECT;
                target = c.transform;
            })
            .AddTo(this);

        searhArea
            .OnTriggerExit2DAsObservable()
            .Where(c => IsTarget(c.gameObject))
            .Subscribe(_ =>
            {
                state = State.IDLE;
            })
            .AddTo(this);

        this.UpdateAsObservable()
            .Where(_ => state == State.DETECT && target != null)
            .Subscribe(_ =>
            {
                //Debug.Log($"[ApproachLinear] target:{target}, me:{searhArea}");
                Vector2 velocity;
                var distance = target.position - transform.position;

                if (distance.magnitude > keepDistanceRadius)
                {
                    var direction = transform.rotation * Vector3.up * speed;
                    var r = Vector3.Cross(distance, direction).z;
                    velocity = Quaternion.Euler(0, 0, -rotateSpeed * r) * direction;
                }
                else
                {
                    velocity = Vector2.zero;
                }

                body.velocity = velocity;

            });

        this.UpdateAsObservable()
            .Where(_ => state == State.IDLE)
            .Subscribe(_ =>
            {
                //?
                body.velocity = Vector2.zero;
            });
    }

    private bool IsTarget(GameObject obj)
    {
        return obj.tag == targetTag;
    }
}

//TODO：見えている場合だけにしたいときはこれを実装
//if (Physics.Linecast(transform.position, target.position))
//{
//    Debug.Log("blocked");
//}