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
    private float regeneratorPerSecond;
    [SerializeField]
    private float moneyPerHealth = 2.5f;

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

    public float NeedMoneyToRepair()
    {
        var max = (int)(Player.Instance.Body.LostHealth() * moneyPerHealth);
        var money = InventoryManager.Instance.Money;

        return Mathf.Min(max, money);
    }

    public void AddRegenerator(float volume)
    {
        regeneratorPerSecond += volume;
    }

    public void Repair()
    {
        var body = Player.Instance.Body;
        var needmoney = NeedMoneyToRepair();

        if (InventoryManager.Instance.TryToPay(needmoney))
        {
            body.RepairDamage(body.LostHealth()); //やさしさ(全快)
            
            //body.RepairDamage(needmoney / moneyPerHealth); //現実
        }
    }
}
