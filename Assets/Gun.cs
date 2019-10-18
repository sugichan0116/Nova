using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;


public class Gun : MonoBehaviour
{
    [SerializeField]
    private Bullet prefab;
    public float speed;
    public int frame;
    public Subject<Vector3> onShoot = new Subject<Vector3>();
    public Body target;

    public Bullet Prefab { get => prefab; }

    // Start is called before the first frame update
    void Start()
    {
        onShoot
            .SampleFrame(frame)
            .Subscribe(direction => {
                var bullet = Instantiate(Prefab, transform.position, transform.localRotation);
                bullet.target = target;
                //ここらへんなかなかやばい
                //layer + tag で認識するシステムの構築が必要
                //bullet.gameObject.layer = 
                transform.localRotation = Quaternion.Euler(0, 0, -Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg);
                bullet.Rigidbody2D.velocity = transform.localRotation * Vector2.up * speed;
            })
            .AddTo(this);
    }

    public Sprite Icon()
    {
        //Debug.Log($"[Gun Icon] {prefab}");
        return Prefab?.GetComponent<SpriteRenderer>()?.sprite;
    }
}
