using DG.Tweening;
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
    public bool ignoreTimeScale;
    public UnityEvent onEnd;

    // Start is called before the first frame update
    void Start()
    {
        onStart.Invoke();

        DOVirtual.DelayedCall(during, () => onEnd.Invoke(), ignoreTimeScale);
    }
}
