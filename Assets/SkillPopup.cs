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

        //var button = GetComponent<Button>();
        //bool isOpening = false;

        button.OnPointerEnterAsObservable()
            .Subscribe(_ =>
            {
                Debug.Log($"[Popup] mouse enter");
                popup = popup ?? ResourcesFactory.Instantiate<Window>("skill popup", transform.parent);

                popup.Open();
                popup.GetComponent<UISkillData>().slot = slot;

                //isOpening = true;
                //Observable
                //    .Timer(TimeSpan.FromSeconds(1))
                //    .Subscribe(__ =>
                //    {
                //        isOpening = false;
                //    })
                //    .AddTo(this);
            });

        button.OnPointerExitAsObservable()
            //.Where(_ => !isOpening)
            .Subscribe(_ =>
            {
                popup.Close();
            });
    }
}
