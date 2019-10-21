using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ShootByInput : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var body = GetComponent<Rigidbody2D>();
        var guns = GetComponentsInChildren<Gun>();

        GunManager.Instance
            .onEquiped
            .Subscribe(_ => {
                guns = GetComponentsInChildren<Gun>();
            });

        Observable
            .EveryFixedUpdate()
            .Subscribe(_ => {
                if (Input.GetButton("Fire1"))
                {
                    foreach(var gun in guns)
                    {
                        var target = MouseCursole.Instance.position - transform.position;
                        var point = new GunTarget()
                        {
                            direction = target,
                            relativeSpeed = body.velocity,
                            tag = this.tag,
                        };
                        gun.onShoot.OnNext(point);
                    }
                }
            })
            .AddTo(this);
    }
}
