using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using UniRx.Triggers;

public class Body : MonoBehaviour
{
    [SerializeField]
    private float health = 100;
    private float residueHealth;
    [SerializeField]
    private bool shouldDestroy = true;

    public Subject<Unit> onDestroy = new Subject<Unit>();

    // Start is called before the first frame update
    void Start()
    {
        residueHealth = health;

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
                if(shouldDestroy) Destroy(gameObject);
            });
    }

    public void ReceiveDamage(float damage)
    {
        if (residueHealth <= 0) return;

        residueHealth -= damage;

        if (residueHealth <= 0)
            onDestroy.OnNext(Unit.Default);
    }

    public void RepairDamage(float volume)
    {
        residueHealth += volume;

        if (residueHealth >= health)
            residueHealth = health;
    }

    public string HealthText()
    {
        return $"{(int)residueHealth} / {(int)health}";
    }

    public float NormalizedHealth()
    {
        return residueHealth / health;
    }

    public float LostHealth() => health - residueHealth;
}
