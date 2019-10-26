using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SkillSlotManager : SingletonMonoBehaviour<SkillSlotManager>
{
    public Transform root;

    private IEnumerable<SkillSlot> slots;

    public IEnumerable<SkillSlot> SkillSlots()
    {
        if(slots == null)
        {
            slots = root.Cast<Transform>()
            .Select(t => t.GetComponent<SkillSlot>());
        }

        return slots;
    }
}
