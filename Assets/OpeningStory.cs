using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UniRx;
using UnityEngine.UI;
using DG.Tweening;
using NaughtyAttributes;

public class OpeningStory : MonoBehaviour
{
    [Multiline]
    public List<string> texts;
    private int index;
    private Text text;
    public UnityEvent onFinished;
    public UnityEvent onWaiting;


    public void NextPage()
    {
        if(texts.Count == index)
        {
            onFinished.Invoke();
        }
        else
        {
            text = text ?? GetComponent<Text>();
            text.text = "";
            var newText = texts[index];
            text.DOText(newText, 0.08f * newText.Length)
                .SetEase(Ease.Linear);
            DOVirtual.DelayedCall(0.08f * newText.Length, () => {
                onWaiting.Invoke();
            });
            text.color = Color.clear;
            text.DOColor(Color.white, 1.5f);

        }
        
        index++;
    }
}
