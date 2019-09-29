using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ShootByInput : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
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
                        gun.onShoot.OnNext(target);
                    }
                }
            })
            .AddTo(this);
    }
}
