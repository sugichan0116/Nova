using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;
using System.Linq;
using TMPro;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var body = Player.Instance.GetComponent<Body>();
        var text = GetComponentInChildren<TextMeshProUGUI>();
        var slider = GetComponentInChildren<Slider>();

        if (body == null) return;

        Observable
            .EveryLateUpdate()
            .Subscribe(_ => {
                text.text = body.HealthText();
                slider.value = body.NormalizedHealth();
            });
    }
}
