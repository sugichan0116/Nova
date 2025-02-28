﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class ShootByClose : MonoBehaviour
{
    [SerializeField]
    TargetDetector ai;
    [SerializeField]
    float radius = 16f;
    [SerializeField]
    float angleOfVisibility = 180f;

    // Start is called before the first frame update
    void Start()
    {
        var guns = GetComponentsInChildren<Gun>();

        Observable
            .Interval(TimeSpan.FromMilliseconds(100)) //yabai
            .Subscribe(_ => {
                var target = ai.Target;
                if (target != null && CanSee(target.position))
                {
                    foreach (var gun in guns)
                    {
                        var point = new GunTarget()
                        {
                            direction = target.position - transform.position,
                            target = target.GetComponent<Body>(),
                            relativeSpeed = Vector2.zero,
                            tag = tag,
                        };

                        gun.OnShoot.OnNext(point);
                    }
                }
            })
            .AddTo(this);
    }

    private bool CanSee(Vector3 target)
    {
        var delta = target - transform.position;
        var front = transform.rotation * Vector3.up;
        var sight = Mathf.Abs(DegreeFrom(front, delta));

        return delta.magnitude < radius //in range
            && sight <= angleOfVisibility / 2f//in sight
            ;
    }

    private float DegreeFrom(Vector3 a, Vector3 b)
    {
        var d = Vector3.Dot(a.normalized, b.normalized);
        return Mathf.Acos(d) * Mathf.Rad2Deg;
    }
}
