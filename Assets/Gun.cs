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
    public Subject<GunTarget> onShoot = new Subject<GunTarget>();

    public Bullet Prefab { get => prefab; }

    // Start is called before the first frame update
    void Start()
    {
        onShoot
            .Sample(TimeSpan.FromMilliseconds(100 * frame))
            .Subscribe(point => {
                var bullet = Instantiate(Prefab, transform.position, transform.localRotation);
                bullet.target = point.target;
                //ここらへんなかなかやばい
                //layer + tag で認識するシステムの構築が必要
                //bullet.gameObject.layer = 
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
}
