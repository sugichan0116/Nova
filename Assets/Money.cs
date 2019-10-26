using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;
using System.Linq;
using UnityEngine.Events;

namespace Nova
{
    public class Money : Item
    {
        public float volume;
        public UnityEvent onGet;

        public override void OnApply(Body body)
        {
            if (!body.CompareTag(TagManager.PLAYER)) return;

            Debug.Log($"[Money] get {volume} by {body} in {this}");
            onGet.Invoke();
            OnDestroy.OnNext(Unit.Default);
            InventoryManager.Instance.EarnMoney(volume);
        }

        // Start is called before the first frame update
        void Start()
        {
            var generator = GetComponent<Generator>();

            OnDestroy
                .Subscribe(_ =>
                {
                    generator.Instantiate();
                    Destroy(gameObject);
                });
        }
    }
}
