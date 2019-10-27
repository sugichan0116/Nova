using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System.Linq;

public class ApproachLinear : TargetDetector
{
    [SerializeField]
    private float speed = 4f;
    [SerializeField]
    private float rotateSpeed = 0.01f;
    [SerializeField]
    private float keepDistanceRadius = 4f;

    // Start is called before the first frame update
    protected void Start()
    {
        base.Start();

        var body = GetComponent<Rigidbody2D>();

        //Move
        this.UpdateAsObservable()
            .Where(_ => state == State.DETECT && Target != null)
            .Subscribe(_ =>
            {
                body.velocity = Velocity(Target);
            });

        this.UpdateAsObservable()
            .Where(_ => state == State.IDLE)
            .Subscribe(_ =>
            {
                body.velocity = Vector2.zero;
            });

    }

    private Vector2 Velocity(Transform target)
    {
        Vector2 velocity = Vector2.zero;
        var distance = target.position - transform.position;

        if (personality == Personality.FEARFUL)
        {
            distance *= -1;
        }

        if (distance.magnitude > 0f)//keepDistanceRadius)
        {
            var direction = transform.rotation * Vector3.up * speed;
            var r = Vector3.Cross(distance, direction).z;
            velocity = Quaternion.Euler(0, 0, -rotateSpeed * r) * direction;
        }

        return velocity;
    }
}

//TODO：見えている場合だけにしたいときはこれを実装
//if (Physics.Linecast(transform.position, target.position))
//{
//    Debug.Log("blocked");
//}