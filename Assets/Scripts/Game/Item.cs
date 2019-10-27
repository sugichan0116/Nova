using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using UniRx.Triggers;
using DG.Tweening;

namespace Nova
{
    public class Item : BodyEffectableObject
    {
        public override void OnApply(Body body)
        {
            OnDestroy.OnNext(Unit.Default);
            return;
        }

        public void Collect(Rigidbody2D target)
        {
            Debug.Log($"[Item Collector] {this}");
            if (this == null) return;
            var ani = transform.DOMove(target.transform.position, 0.4f);

            OnDestroy
                .Subscribe(_ =>
                {
                    ani.Kill();
                });
        }

        // Start is called before the first frame update
        void Start()
        {
            OnDestroy
                .Subscribe(_ =>
                {
                    Destroy(gameObject);
                });
        }
    }
}
