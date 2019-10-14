using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using TMPro;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    public string gunName;
    public Gun gun;
    public bool IsDeveloped;
    public float needGem;

    [Header("Objects")]
    public Image image;
    public TextMeshProUGUI text;
    public GameObject mask;
    public TextMeshProUGUI gemText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Equip()
    {
        if(!IsDeveloped)
        {
            if(InventoryManager.Instance.TryToPay(needGem))
            {
                IsDeveloped = true;
                mask.SetActive(!IsDeveloped);
                CutInBox.Instance.Call("Unlocked", $"{gunName} is on Active!");
            }
        }
        GunManager.Instance.EquipGunToSelectedSlot(gun);
    }

#if UNITY_EDITOR
    [Button]
    private void Reflect()
    {
        text.text = gunName;
        image.sprite = gun.Icon();
        mask.SetActive(!IsDeveloped);
        gemText.text = $"{needGem}";
    }
#endif
}
