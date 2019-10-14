using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using UniRx.Triggers;

public abstract class BodyEffectableObject : MonoBehaviour
{
    public Subject<Unit> onDestroy = new Subject<Unit>();

    public abstract void OnApply(Body body);
}

public class Bullet : BodyEffectableObject
{
    [HideInInspector]
    public Rigidbody2D body;
    public GameObject bomb;
    public float damage;
    public Body target;

    public Rigidbody2D Rigidbody2D
    {
        get
        {
            if (body == null) { body = GetComponent<Rigidbody2D>(); }
            return body;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        onDestroy
            .Subscribe(_ =>
            {
                Instantiate(bomb, transform.position, transform.rotation);
                Destroy(gameObject);
            });
    }

    public float Damage() => damage;

    public override void OnApply(Body body)
    {
        body.ReceiveDamage(Damage());
        onDestroy.OnNext(Unit.Default);
    }
}
