using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class LookForVelocity : MonoBehaviour
{
    void Start()
    {
        var body = GetComponent<Rigidbody2D>();

        Observable
            .EveryFixedUpdate()
            .Subscribe(_ => {
                var dir = body.velocity;
                if (dir.magnitude > 0)
                {
                    var rotate = -Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
                    if (body.rotation - rotate % 180 != 0)
                    {
                        body.SetRotation(rotate);
                    }
                }
            })
            .AddTo(this);
    }
}
