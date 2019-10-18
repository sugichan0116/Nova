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
    [SerializeField]
    private Image image;
    [SerializeField]
    private TextMeshProUGUI text;
    [SerializeField]
    private GameObject mask;
    [SerializeField]
    private TextMeshProUGUI gemText;

    public bool CanUnlock { get => inheritance.Count == 0 || inheritance.All(s => s.IsUnlocked); }
    public bool IsUnlocked { get => isUnlocked; }
    public Skill Skill { get => skill; }

    // Start is called before the first frame update
    void Start()
    {
        Reflect();
    }

    public void Unlock()
    {
        if(!CanUnlock) MessageLog.Print("[Error] Incomplete research");
        if (IsUnlocked) MessageLog.Print("[Error] already Unlocked");

        if (CanUnlock && !IsUnlocked)
        {
            if (InventoryManager.Instance.TryToPay(Skill.gem))
            {
                isUnlocked = true;
                Reflect();
                AchievementBox.Print("Unlocked", $"{Skill.skillName} is on Active!");

                if((Skill is GunSkill) == false)
                {
                    Player.Instance.ApplySkill.Apply(skill);
                }
            }
        }
    }

    public void Apply()
    {
        if(Skill is GunSkill gunSkill)
        {
            GunManager.Instance.EquipGunToSelectedSlot(gunSkill.gun);
            MessageLog.Print($"[Equiped] {gunSkill.skillName}");
        }
    }

    [Button]
    private void Reflect()
    {
        text.text = Skill.skillName;
        image.sprite = Skill.Icon();
        mask.SetActive(!IsUnlocked);
        gemText.text = $"{Skill.gem}";
    }
}