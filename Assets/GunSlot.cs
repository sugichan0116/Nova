using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSlot : MonoBehaviour
{
    private Gun gun;
    public bool isUnlocked;
    public float needGem;

    public Gun Gun
    {
        get
        {
            if (!isUnlocked) return null;

            if (gun == null)
            {
                gun = GetComponentInChildren<Gun>();
            }
            return gun;
        }
    }

    public void EquipGun(Gun gun)
    {
        if (!isUnlocked) return;

        if (Gun != null)
        {
            Destroy(Gun.gameObject);
        }

        Instantiate(gun, gameObject.transform);
    }

    public void Unlock()
    {
        if(!isUnlocked)
        {
            if (InventoryManager.Instance.TryToPay(needGem))
            {
                isUnlocked = true;
                CutInBox.Instance.Call("Unlocked", $"Equip slot is Unlocked!");
            }
        }
    }
}
