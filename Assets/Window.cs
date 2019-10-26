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

    //public bool shouldPause;
    //private bool isPausing;

    [Header("Animation")]
    //public bool isEnable = false;
    [ReorderableList]
    public List<UIAnimation> animations = new List<UIAnimation>();

    public void Open()
    {
        gameObject.SetActive(true);
        
        if(/*isEnable ||*/ animations.Count() > 0)
        {
            //isPausing = TimeScaleManager.Instance.IsPausing();
            //if (shouldPause && isPausing) TimeScaleManager.Instance.Restart();

            var time = animations.Max(anim => anim.Show());
            Observable
                .Timer(TimeSpan.FromSeconds(time))
                .Subscribe(_ =>
                {
                    onOpen.Invoke();
                    //if (shouldPause) TimeScaleManager.Instance.Pause();
                })
                .AddTo(this);
        }
        else
        {
            onOpen.Invoke();
            //if (shouldPause) TimeScaleManager.Instance.Pause();
        }
    }

    public void Close()
    {
        //if (shouldPause) TimeScaleManager.Instance.Restart();

        if (/*isEnable ||*/ animations.Count() > 0)
        {
            var time = animations.Max(anim => anim.Hide());
            Observable
                .Timer(TimeSpan.FromSeconds(time))
                .Subscribe(_ =>
                {
                    onClose.Invoke();
                    //if (shouldPause && isPausing) TimeScaleManager.Instance.Pause();////
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
