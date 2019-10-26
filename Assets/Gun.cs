using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class Gun : MonoBehaviour
{
    [SerializeField]
    private Bullet prefab;
    public float speed;
    public int frame;
    [NonSerialized]
    private Subject<GunTarget> onShoot;

    public Bullet Prefab { get => prefab; }
    public Subject<GunTarget> OnShoot
    {
        get
        {
            if (onShoot == null) onShoot = new Subject<GunTarget>();
            return onShoot;
        }
        set => onShoot = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        OnShoot
            .Sample(TimeSpan.FromMilliseconds(100 * frame))
            .Subscribe(point => {
                var bullet = Instantiate(Prefab, transform.position, transform.localRotation);
                bullet.target = point.target;
                bullet.tag = point.tag;
                transform.localRotation = Quaternion.Euler(0, 0, DegreeFrom(point.direction));
                var velocity = transform.localRotation * Vector2.up * speed;
                bullet.Rigidbody2D.velocity = velocity + point.relativeSpeed;
            })
            .AddTo(this);
    }

    public Sprite Icon()
    {
        //Debug.Log($"[Gun Icon] {prefab}");
        return Prefab?.GetComponent<SpriteRenderer>()?.sprite;
    }

    private float DegreeFrom(Vector2 direction)
    {
        return -Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
    }
}

public class GunTarget
{
    public Vector3 direction;
    public Body target;
    public Vector3 relativeSpeed;
    public string tag;
}
