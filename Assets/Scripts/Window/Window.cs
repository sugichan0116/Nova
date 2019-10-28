using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UniRx;
using NaughtyAttributes;
using System.Linq;
using System;

public class Window : MonoBehaviour
{
    public UnityEvent onOpen = new UnityEvent();
    public UnityEvent onClose = new UnityEvent();

    public bool shouldPause;

    [Header("Animation")]
    [ReorderableList]
    public List<UIAnimation> animations = new List<UIAnimation>();

    public void Open()
    {
        gameObject.SetActive(true);
        
        if(animations.Count() > 0)
        {
            var time = animations.Max(anim => anim.Show());
            Observable
                .Timer(TimeSpan.FromSeconds(time))
                .Subscribe(_ =>
                {
                    onOpen.Invoke();
                    if (shouldPause) TimeScaleManager.Instance.Pause();
                })
                .AddTo(this);
        }
        else
        {
            onOpen.Invoke();
            if (shouldPause) TimeScaleManager.Instance.Pause();
        }
    }

    public void Close()
    {
        if (shouldPause) TimeScaleManager.Instance.Restart();

        if (animations.Count() > 0)
        {
            var time = animations.Max(anim => anim.Hide());
            Observable
                .Timer(TimeSpan.FromSeconds(time))
                .Subscribe(_ =>
                {
                    onClose.Invoke();
                    gameObject.SetActive(false);
                })
                .AddTo(this);
        }
        else
        {
            onClose.Invoke();
            gameObject.SetActive(false);
        }
    }
}
