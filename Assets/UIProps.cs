using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UniRx;
using UniRx.Triggers;
using TMPro;

public class UIProps : MonoBehaviour
{
    public StateProps order;
    public string prefix, suffix;

    // Start is called before the first frame update
    void Start()
    {
        var text = GetComponent<TextMeshProUGUI>();

        this
            .UpdateAsObservable()
            .Subscribe(_ =>
            {
                text.text = $"{prefix}{StoreManager.Instance.Dispatch(order)}{suffix}";
            });
    }
}

public enum StateProps
{
    MONEY_HAVING,
    MONEY_TO_REPAIR,
    MONEY_TO_UNLOCK_GUNSLOT,
}