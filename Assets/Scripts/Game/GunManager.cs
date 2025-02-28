﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UniRx;
using UniRx.Triggers;
using System;

public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                Type t = typeof(T);

                instance = (T)FindObjectOfType(t);
                if (instance == null)
                {
                    Debug.LogError(t + " をアタッチしているGameObjectはありません");
                }
            }

            return instance;
        }
    }
    virtual protected void Awake()
    {
        // 他のGameObjectにアタッチされているか調べる.
        // アタッチされている場合は破棄する.
        if (this != Instance)
        {
            Destroy(this);
            //Destroy(this.gameObject);
            Debug.LogError(
                typeof(T) +
                " は既に他のGameObjectにアタッチされているため、コンポーネントを破棄しました." +
                " アタッチされているGameObjectは " + Instance.gameObject.name + " です.");
            return;
        }

        // なんとかManager的なSceneを跨いでこのGameObjectを有効にしたい場合は
        // ↓コメントアウト外してください.
        //DontDestroyOnLoad(this.gameObject);
    }

}

public class GunManager : SingletonMonoBehaviour<GunManager>
{
    public Subject<Unit> onEquiped = new Subject<Unit>();
    [HideInInspector]
    public GunSlot selectedSlot;

    public void EquipGunToSelectedSlot(GunSkill gun)
    {
        EquipGunToSlot(gun, selectedSlot);
    }

    public void EquipGunToSlot(GunSkill gun, GunSlot slot)
    {
        slot.EquipGun(gun);
        onEquiped.OnNext(Unit.Default);
    }

    public void UnlockSlot()
    {
        if (InventoryManager.Instance.TryToPay(selectedSlot.needGem))
        {
            selectedSlot.isUnlocked = true;
            AchievementBox.Print("Unlock", "New Weapon Slot!");
            onEquiped.OnNext(Unit.Default);
        }
    }
}
