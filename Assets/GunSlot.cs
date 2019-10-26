using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSlot : MonoBehaviour
{
    public int slotID;
    private Gun gun;
    public bool isUnlocked;
    public float needGem;

    public GunSkill skill;

    private void Start()
    {
        EquipGun(skill);
    }

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

    public void EquipGun(GunSkill skill)
    {
        if (skill == null) return;

        this.skill = skill;

        EquipGun(skill.gun);
    }

    private void EquipGun(Gun gun)
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
                AchievementBox.Print("Unlock", $"Equip slot is Unlocked!");
            }
        }
    }
}
