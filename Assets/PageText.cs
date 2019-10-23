using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UniRx;

public class PageText : MonoBehaviour
{
    public PageSequencer sequencer;

    // Start is called before the first frame update
    void Start()
    {
        var text = GetComponent<TextMeshProUGUI>();

        sequencer
            .ObserveEveryValueChanged(s => s.PageToString())
            .Subscribe(page => {
                text.text = page;
            })
            .AddTo(this);
    }
}
