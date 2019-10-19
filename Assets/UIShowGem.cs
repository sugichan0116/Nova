using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class UIShowGem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var a = InventoryManager.Instance;
        var text = GetComponent<TextMeshProUGUI>();

        this.UpdateAsObservable()
            .Subscribe(_ =>
            {
                text.text = $"{a.Money}";
            });
    }
}
