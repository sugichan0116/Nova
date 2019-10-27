using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Nova
{
    public class ItemCollector : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            var collider = GetComponent<Collider2D>();
            var player = Player.Instance.GetComponent<Rigidbody2D>();

            collider
                .OnTriggerEnter2DAsObservable()
                .Select(c => c.gameObject.GetComponent<Item>())
                .Where(item => item != null)
                .Subscribe(item =>
                {
                    Observable
                        .EveryFixedUpdate()
                        .Where(_ => player.velocity.magnitude < 1f)
                        .Take(1)
                        .Subscribe(_ =>
                        {
                            item.Collect(player);
                        })
                        .AddTo(this);
                })
                .AddTo(this);
        }
    }
}


