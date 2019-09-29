using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using UniRx.Triggers;

public class ItemGun : Item
{
    public Gun gun;

    public override void OnApply(Body body)
    {
        Debug.Log("appp");
        onDestroy.OnNext(Unit.Default);
        GunManager.Instance.EquipGun(gun);
    }

    // Start is called before the first frame update
    void Start()
    {
        onDestroy
            .Subscribe(_ =>
            {
                Destroy(gameObject);
            });
    }
}
