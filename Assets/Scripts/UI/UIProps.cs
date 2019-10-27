using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UniRx;
using UniRx.Triggers;
using TMPro;
using System;

public class UIProps : MonoBehaviour
{
    public StateProps order;
    public string prefix, suffix;
    public bool isAnime;
    private float value = 0f;

    // Start is called before the first frame updatedw
    void Start()
    {
        var text = GetComponent<TextMeshProUGUI>();

        if (isAnime) this.UpdateAsObservable()
            .Subscribe(_ =>
            {
                var origin = StoreManager.Instance.Get(order);
                var delta = origin - value;
                var based = Mathf.Pow(10, Mathf.Max(1, (int)Mathf.Log10(Mathf.Abs(delta)) - 1));
                value += Mathf.Min(based, Mathf.Abs(delta)) * Mathf.Sign(delta);
            })
            .AddTo(this);

        if (!isAnime) this.UpdateAsObservable()
            .Subscribe(_ =>
            {
                value = StoreManager.Instance.Get(order);
            })
            .AddTo(this);

        this.UpdateAsObservable()
            .Subscribe(_ =>
            {
                text.text = $"{prefix}{(int)value}{suffix}";
            })
            .AddTo(this);

    }
}

public enum StateProps
{
    MONEY_HAVING = 0,
    MONEY_TO_REPAIR,
    MONEY_TO_UNLOCK_GUNSLOT,

    PLAYER_MOVE_SPEED = 100,

    MARKET_NAME = 200,
    MARKET_TAX,
    MARKET_DEAL_WITH,
}