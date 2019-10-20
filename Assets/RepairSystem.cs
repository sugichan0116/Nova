using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using NaughtyAttributes;
using System.Linq;
using System;

public class RepairSystem : SingletonMonoBehaviour<RepairSystem>
{
    [SerializeField]
    public float regeneratorPerSecond;

    private void Start()
    {
        var body = Player.Instance.Body;

        Observable
            .EveryFixedUpdate()
            .Subscribe(_ =>
            {
                body.RepairDamage(regeneratorPerSecond * Time.deltaTime);
            })
            .AddTo(this);
    }

    public void Repair()
    {
        var body = Player.Instance.Body;
        var needmoney = StoreManager.Instance.Get(StateProps.MONEY_TO_REPAIR);
        var inventory = InventoryManager.Instance;

        if (inventory.TryToPay(needmoney))
        {
            body.RepairDamage(body.LostHealth());
        }
    }
}
