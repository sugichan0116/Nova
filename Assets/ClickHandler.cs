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
    AWAKE_MARKET,
    AWAKE_DEVELOP,
    AWAKE_SLOT_UNLOCK,
    ACTION_REPAIR=100,
    ACTION_UNLOCK_GUNSLOT,
    ACTION_MARKET_DEAL_1=200,
    ACTION_MARKET_DEAL_10,
    ACTION_MARKET_DEAL_100,
    ACTION_MARKET_DEAL_1000,
}