using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class SelectGunSlot : MonoBehaviour
{
    public GunSlot slot;
    public Image icon;
    public GameObject mask;

    //private void Start()
    //{
    //    SetImage();

    //    GunManager.Instance
    //        .onEquiped
    //        .Subscribe(_ => { SetImage(); })
    //        .AddTo(this);
    //}

    private void Update()
    {
        SetImage();
    }

    public void SelectSlot()
    {
        GunManager.Instance.selectedSlot = slot;
    }

    private void SetImage()
    {
        //Debug.Log($"[Select Slot] update {this}");
        icon.sprite = slot.Gun?.Icon();
        icon.gameObject.SetActive(slot.Gun != null);
        mask.SetActive(!slot.isUnlocked);
    }
}
