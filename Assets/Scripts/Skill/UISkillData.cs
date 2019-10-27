using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UISkillData : MonoBehaviour
{
    [HideInInspector]
    public SkillSlot slot;

    public TextMeshProUGUI title, content, tips;

    // Start is called before the first frame update
    void Start()
    {
        var skill = slot.Skill;
        title.text = skill.skillName;
        content.text = skill.ToDescription();
        tips.text = skill.ToTips();
    }
}
