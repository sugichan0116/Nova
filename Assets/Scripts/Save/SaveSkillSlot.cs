using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class SkillUnlockState
{
    public Dictionary<int, bool> unlocks;

    public SkillUnlockState()
    {
        unlocks = new Dictionary<int, bool>();
    }
}

public class SaveSkillSlot : SavePort
{
    protected override PackageObject Save()
    {
        var skills = new SkillUnlockState();

        var slots = SkillSlotManager.Instance.SkillSlots();

        foreach(var slot in slots)
        {
            skills.unlocks.Add(slot.Skill.skillID, slot.IsUnlocked);
        }

        return new PackageObject(typeof(SkillUnlockState), skills);
    }

    protected override void Load(PackageObject package)
    {
        var state = (SkillUnlockState)(package.data);

        var slots = SkillSlotManager.Instance.SkillSlots();

        foreach (var slot in slots)
        {
            if(state.unlocks.TryGetValue(slot.Skill.skillID, out var isUnlocked))
            {
                slot.IsUnlocked = isUnlocked;
            }
        }
    }
}
