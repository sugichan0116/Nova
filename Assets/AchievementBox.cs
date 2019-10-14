using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using System;

public class AchievementBox : SingletonMonoBehaviour<AchievementBox>
{
    public GameObject box;
    public TextMeshProUGUI subText, text;
    
    public static void Print(string sub, string main)
    {
        Instance.Call(sub, main);
    }

    private void Call(string sub, string main)
    {
        box.SetActive(true);
        subText.text = sub;
        text.text = main;

        Observable
            .Timer(TimeSpan.FromSeconds(2f))
            .Subscribe(_ =>
            {
                box.SetActive(false);
            })
            .AddTo(this);
    }
}
