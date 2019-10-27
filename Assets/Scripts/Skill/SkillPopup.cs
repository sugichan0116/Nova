using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;
using System;

public class SkillPopup : MonoBehaviour
{
    public Button button;
    public SkillSlot slot;

    // Start is called before the first frame update
    void Start()
    {
        Window popup = null;

        button.OnPointerEnterAsObservable()
            .Subscribe(_ =>
            {
                //Debug.Log($"[Popup] mouse enter");
                if(popup == null)
                {
                    var canvas = GetComponentInParent<Canvas>().transform;
                    popup = ResourcesFactory.Instantiate<Window>("skill popup", canvas);
                }

                popup.Open();
                popup.GetComponent<UISkillData>().slot = slot;

            });

        button.OnPointerExitAsObservable()
            .Subscribe(_ =>
            {
                popup.Close();
            });
    }
}
