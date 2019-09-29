using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;


public class Gun : MonoBehaviour
{
    public Bullet prefab;
    public float speed;
    public int frame;
    public Subject<Vector3> onShoot = new Subject<Vector3>();
    //public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        onShoot
            .SampleFrame(frame)
            .Subscribe(target => {
                var bullet = Instantiate(prefab, transform.position, transform.localRotation);
                transform.localRotation = Quaternion.Euler(0, 0, -Mathf.Atan2(target.x, target.y) * Mathf.Rad2Deg);
                bullet.Rigidbody2D.velocity = transform.localRotation * Vector2.up * speed;
            })
            .AddTo(this);
    }
}
