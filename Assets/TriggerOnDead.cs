using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Events;

public class TriggerOnDead : MonoBehaviour
{
    public UnityEvent onDead;

    // Start is called before the first frame update
    void Start()
    {
        var body = GetComponent<Body>();

        body.onDestroy
            .Subscribe(_ =>
            {
                onDead.Invoke();
            })
            .AddTo(this);
    }
}
