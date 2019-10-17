using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class SkillSlot : MonoBehaviour
{
    [SerializeField]
    private Skill skill;
    [SerializeField]
    private bool isUnlocked;
    [SerializeField]
    private List<SkillSlot> inheritance;

    [Header("Objects")]
    public Image image;
    public TextMeshProUGUI text;
    public GameObject mask;
    public TextMeshProUGUI gemText;

    public bool CanUnlock { get => inheritance.Count == 0 || inheritance.All(s => s.IsUnlocked); }
    public bool IsUnlocked { get => isUnlocked; }

    // Start is called before the first frame update
    void Start()
    {
        Reflect();
    }

    public void Unlock()
    {
        if(!CanUnlock) MessageLog.Print("Error");
        if (IsUnlocked) MessageLog.Print("Unlocked!");

        if (CanUnlock && !IsUnlocked)
        {
            if (InventoryManager.Instance.TryToPay(skill.gem))
            {
                isUnlocked = true;
                mask.SetActive(!IsUnlocked);
                AchievementBox.Print("Unlocked", $"{skill.skillName} is on Active!");
            } else MessageLog.Print("Gem required!!");
        }
    }

    public void Equip()
    {
        if(skill is GunSkill gunSkill)
        {
            GunManager.Instance.EquipGunToSelectedSlot(gunSkill.gun);
            MessageLog.Print($"[Equiped] {gunSkill.skillName}");
        }
    }

//#if UNITY_EDITOR
    [Button]
    private void Reflect()
    {
        text.text = skill.skillName;
        image.sprite = skill.Icon();
        mask.SetActive(!IsUnlocked);
        gemText.text = $"{skill.gem}";
    }
//#endif
}