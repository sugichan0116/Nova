using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using UniRx.Triggers;

public class Item : BodyEffectableObject
{
    public override void OnApply(Body body)
    {
        Debug.Log("apvvvvpp");

        onDestroy.OnNext(Unit.Default);
        return;
    }

    // Start is called before the first frame update
    void Start()
    {
        onDestroy
            .Subscribe(_ =>
            {
                Destroy(gameObject);
            });
    }
}
