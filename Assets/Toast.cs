using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Events;

public class Toast : MonoBehaviour
{
    public UnityEvent onStart;
    public float during;
    public UnityEvent onEnd;

    // Start is called before the first frame update
    void Start()
    {
        onStart.Invoke();

        Observable
            .Timer(TimeSpan.FromSeconds(during))
            .Subscribe(_ =>
            {
                onEnd.Invoke();
            })
            .AddTo(this);
    }
}
