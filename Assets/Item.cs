using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using UniRx.Triggers;

namespace Nova
{
    public class Item : BodyEffectableObject
    {
        private Rigidbody2D attachedRigidbody;

        public Rigidbody2D AttachedRigidbody {
            get
            {
                if(attachedRigidbody == null)
                {
                    attachedRigidbody = GetComponent<Rigidbody2D>();
                }

                return attachedRigidbody;
            }
        }

        public override void OnApply(Body body)
        {
            onDestroy.OnNext(Unit.Default);
            return;
        }

        // Start is called before the first frame update
        void Start()
        {
            onDestroy
                .Subscribe(_ =>
                {
                    Destroy(gameObject);
                });
        }


    }
}
