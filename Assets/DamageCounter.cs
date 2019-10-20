using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

public class DamageCounter : MonoBehaviour
{
    public TextMeshPro text;

    // Start is called before the first frame update
    void Start()
    {
        var body = GetComponent<Body>();

        Observable
            .EveryUpdate()
            .Select(_ => body.LostHealth())
            .Buffer(2, 1)
            .Select(h => (h[1] - h[0]) / Time.deltaTime)
            .Buffer(100, 1)
            .Select(b => b.Average())
            .Sample(TimeSpan.FromSeconds(0.1f))
            .Buffer(5, 1)
            .Select(b => b.Max())
            .Subscribe(dps =>
            {
                text.text = $"dps\n{(int)dps}\ndpm\n{(int)(dps * 60)}";
            })
            .AddTo(this);
    }
}
