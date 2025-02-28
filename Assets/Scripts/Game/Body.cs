﻿using System.Collections;
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

    public float Health {
        get => health;
        set
        {
            Debug.Log($"[Health] set {value} ({this})");
            var lost = LostHealth();
            health = value;
            residueHealth = value - lost;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        residueHealth = Health;
        
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

        if (residueHealth >= Health)
            residueHealth = Health;
    }

    public void ImproveHealth(float volume)
    {
        Health += volume;
        residueHealth += volume;
    }

    public string HealthText()
    {
        return $"{(int)residueHealth} / {(int)Health}";
    }

    public float NormalizedHealth()
    {
        return residueHealth / Health;
    }

    public float LostHealth() => Health - residueHealth;
}
