using System.Collections;
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
    public GunSlot selectedSlot;

    public void EquipGun(Gun gunPrefab)
    {
        var slot = GetComponentsInChildren<GunSlot>()
            .Where(s => s.Gun == null)
            .FirstOrDefault();

        if (slot == null) return;

        EquipGunToSlot(gunPrefab, slot);
    }

    public void EquipGunToSelectedSlot(Gun gun)
    {
        EquipGunToSlot(gun, selectedSlot);
    }

    private void EquipGunToSlot(Gun gun, GunSlot slot)
    {
        slot.EquipGun(gun);
        onEquiped.OnNext(Unit.Default);
    }

    public void UnlockSlot()
    {
        if(InventoryManager.Instance.TryToPay(selectedSlot.needGem))
        {
            selectedSlot.isUnlocked = true;
            AchievementBox.Print("Unlock", "New Weapon Slot!");
            onEquiped.OnNext(Unit.Default);
        }
    }
}
