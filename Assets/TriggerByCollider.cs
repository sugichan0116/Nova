using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using UniRx;
using UniRx.Triggers;
using System;
using NaughtyAttributes;

public class TriggerByCollider : MonoBehaviour
{
    //[Dropdown("Tags")]
    //public string targetTag;
    //private string[] Tags = TagManager.TAGS;

    public UnityEvent onFirst;

    // Start is called before the first frame update
    void Start()
    {
        var collider = GetComponent<Collider2D>();

        collider
            .OnTriggerEnter2DAsObservable()
            //.Where(c => c.gameObject.tag == targetTag)
            .Select(c => c.gameObject.GetComponent<Player>())
            .Where(p => p != null)
            .Take(1)
            .Subscribe(c =>
            {
                //Debug.Log($"[Trigger] enter ({c.gameObject}){c.gameObject.tag} vs {targetTag}");
                onFirst.Invoke();
            })
            .AddTo(this);
    }
}
