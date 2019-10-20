using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using UniRx;
using UniRx.Triggers;
using System;

public class TriggerByCollider : MonoBehaviour
{
    public UnityEvent onFirst;

    // Start is called before the first frame update
    void Start()
    {
        var collider = GetComponent<Collider2D>();

        collider
            .OnTriggerEnter2DAsObservable()
            .Take(1)
            .Subscribe(_ =>
            {
                onFirst.Invoke();
            })
            .AddTo(this);
    }
}
