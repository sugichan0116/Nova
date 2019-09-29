using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using UniRx.Triggers;

public class Body : MonoBehaviour
{
    [SerializeField]
    float health = 100;

    public Subject<Unit> onDestroy = new Subject<Unit>();

    // Start is called before the first frame update
    void Start()
    {
        this
            .OnTriggerEnter2DAsObservable()
            .Subscribe(collider =>
            {
                var effectObject = collider.gameObject.GetComponent<BodyEffectableObject>();
                if (effectObject != null)
                {
                    effectObject.OnApply(this);
                }
            });

        //test
        onDestroy
            .Subscribe(_ =>
            {
                Destroy(gameObject);
            });
    }

    public void ReceiveDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
            onDestroy.OnNext(Unit.Default);
    }
}
