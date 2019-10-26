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
    public UnityEvent onFinished;
    public UnityEvent onWaiting;

    private int index;
    private Text text;
    private bool isPlaying = false;
    private Sequence sequence;

    public void NextPage()
    {
        if(isPlaying)
        {
            sequence.Kill();
            text.text = texts[index - 1];
            text.color = Color.white;
            isPlaying = false;
            return;
        }

        if(texts.Count == index)
        {
            onFinished.Invoke();

            index++;
        }
        else
        {
            isPlaying = true;
            text = text ?? GetComponent<Text>();
            text.text = "";
            text.color = Color.clear;

            var newText = texts[index];
            var time = 0.08f * newText.Length;

            sequence = DOTween.Sequence();

            sequence.Append(text.DOText(newText, time).SetEase(Ease.Linear));
            sequence.Join(DOVirtual.DelayedCall(time, () => {
                onWaiting.Invoke();
                isPlaying = false;
            }));
            sequence.Join(text.DOColor(Color.white, Mathf.Min(1.5f, time)));

            index++;
        }
    }
}
