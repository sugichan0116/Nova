using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using System;
using System.Linq;
using DG.Tweening;

public class AchievementBox : SingletonMonoBehaviour<AchievementBox>
{
    public class Message
    {
        public string sub, main;
    }

    public Window box;
    public TextMeshProUGUI subText, text;
    public float during = 2f;

    private Queue<Message> messages = new Queue<Message>();

    private void Start()
    {
        Observable.EveryUpdate()
            .Where(_ => messages.Count() > 0 && !box.isActiveAndEnabled)
            .Select(_ => messages.Dequeue())
            .Subscribe(message =>
            {
                box.Open();
                subText.text = message.sub;
                text.text = message.main;

                var onClose = box.onClose.AsObservable().Select(_ => (long)0);

                DOVirtual.DelayedCall(during, () => box.Close(), true);
            })
            .AddTo(this);
    }

    public static void Print(string sub, string main)
    {
        Instance.Call(sub, main);
    }

    private void Call(string sub, string main)
    {
        messages.Enqueue(new Message() { sub = sub, main = main });
    }
}
