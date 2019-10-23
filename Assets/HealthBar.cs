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
    public Window warning;
    public float warningThreshold = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        var body = Player.Instance.GetComponent<Body>();
        var text = GetComponentInChildren<TextMeshProUGUI>();
        var slider = GetComponentInChildren<Slider>();
        var image = GetComponent<Image>();

        if (body == null) return;

        Observable
            .EveryLateUpdate()
            .Sample(TimeSpan.FromSeconds(0.01f))
            .Subscribe(_ => {
                text.text = body.HealthText();

                var delta = body.NormalizedHealth() - slider.value;
                slider.value += Mathf.Min(0.01f, Mathf.Abs(delta)) * Mathf.Sign(delta); 
                //body.NormalizedHealth();
            });


        body.ObserveEveryValueChanged(b => b.NormalizedHealth() <= warningThreshold)
            .Skip(1)
            .Subscribe(isWarning =>
            {
                if (isWarning) warning.Open();
                else warning.Close();
            })
            .AddTo(this);

        body.ObserveEveryValueChanged(b => b.NormalizedHealth())
            .Subscribe(health =>
            {
                image.color = (health <= warningThreshold) ? Color.red : Color.white;
            })
            .AddTo(this);
    }
}
