using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class ShootByClose : MonoBehaviour
{
    [SerializeField]
    float radius = 16f;
    [SerializeField]
    float angleOfVisibility = 180f;

    // Start is called before the first frame update
    void Start()
    {
        var guns = GetComponentsInChildren<Gun>();

        Observable
            //.EveryFixedUpdate()
            .Interval(TimeSpan.FromMilliseconds(100))
            .Subscribe(_ => {
                var target = Player.Instance.transform.position;
                if (CanSee(target))
                {
                    foreach (var gun in guns)
                    {
                        var point = new GunTarget()
                        {
                            direction = target - transform.position,
                            target = Player.Instance.Body,
                            relativeSpeed = Vector2.zero
                        };

                        gun.onShoot.OnNext(point);
                    }
                }
            })
            .AddTo(this);
    }

    private bool CanSee(Vector3 target)
    {
        var delta = target - transform.position;
        var front = transform.rotation * Vector3.up;

        return delta.magnitude < radius //in range
            && Mathf.Abs(DegreeFrom(front, delta)) <= angleOfVisibility / 2f//in sight
            ;
    }

    private float DegreeFrom(Vector3 a, Vector3 b)
    {
        var d = Vector3.Dot(a.normalized, b.normalized);
        return Mathf.Acos(d) * Mathf.Rad2Deg;
    }
}
