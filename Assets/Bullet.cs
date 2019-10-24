using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using UniRx.Triggers;
using UnityEngine.Events;

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
    public bool shouldDestroy = true;

    public UnityEvent onHit;

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
                if (shouldDestroy) Destroy(gameObject);
            });
    }

    public float Damage() => damage;

    public override void OnApply(Body body)
    {
        if (RelationshipManager.IsFriend(body.tag, tag)) return;

        body.ReceiveDamage(Damage());
        onHit.Invoke();
        onDestroy.OnNext(Unit.Default);
    }

    private bool IsAlly(GameObject obj)
    {
        return obj.CompareTag(tag);
    }
}
