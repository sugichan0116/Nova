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
                .OnTriggerStay2DAsObservable()
                .Where(_ => player.velocity.magnitude < 10f)
                .Select(c => c.gameObject.GetComponent<Item>())
                .Where(item => item != null)
                .Subscribe(item =>
                {
                    //Debug.Log($"[Item Collector] {item}");
                    var body = item.AttachedRigidbody;
                    var direction = -(body.transform.position - transform.position);
                    body.velocity = (Vector2)direction * (4f) + player.velocity;
                })
                .AddTo(this);
        }
    }
}


