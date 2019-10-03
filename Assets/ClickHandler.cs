using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UniRx;
using UniRx.Triggers;
using System;

public class ClickHandler : MonoBehaviour
{
    public ClickMessage message;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void Dispatch()
    {
        ClickManager.Instance.ListenMessage(message);
    }
}

public enum ClickMessage
{
    AWAKE_MACHINESHOP=0,
    AWAKE_REPAIR,
    ACTION_REPAIR=100
}