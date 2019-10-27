using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class GunSlotState
{
    public int gunID;
    public bool isUnlocked;

    public GunSlotState(int _gunID, bool _isUnlocked)
    {
        gunID = _gunID;
        isUnlocked = _isUnlocked;
    }
}

[System.Serializable]
public class GunSlotStateList
{
    public Dictionary<int, GunSlotState> states;

    public GunSlotStateList()
    {
        states = new Dictionary<int, GunSlotState>();
    }
}

public class SaveGunSlot : SavePort
{
    protected override PackageObject Save()
    {
        var state = new GunSlotStateList();

        var slots = Player.Instance.GetComponentsInChildren<GunSlot>();

        foreach(var slot in slots)
        {
            var gunState = new GunSlotState(slot.skill?.skillID ?? 0, slot.isUnlocked);
            state.states.Add(slot.slotID, gunState);
        }

        return new PackageObject(typeof(GunSlotStateList), state);
    }

    protected override void Load(PackageObject package)
    {
        var state = (GunSlotStateList)(package.data);

        var slots = Player.Instance.GetComponentsInChildren<GunSlot>();
        var library = SkillSlotManager.Instance.SkillSlots().Select(s => s.Skill);

        foreach (var slot in slots)
        {
            if(state.states.TryGetValue(slot.slotID, out var gunState))
            {
                slot.isUnlocked = gunState.isUnlocked;

                var skill = library.FirstOrDefault(s => s.skillID == gunState.gunID);
                if (skill != null && skill is GunSkill gunSkill)
                {
                    GunManager.Instance.EquipGunToSlot(gunSkill, slot);
                }
            }
        }
    }
}
