using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class MoveByInput : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        var body = GetComponent<Rigidbody2D>();
        Observable
            .EveryFixedUpdate()
            .Subscribe(_ => {
                var dir = new Vector2(
                    Input.GetAxis("Horizontal") * speed,
                    Input.GetAxis("Vertical") * speed
                );

                var a = 0.95f;
                dir = Camera.main.transform.rotation* dir;
                dir = a * body.velocity + (1 - a) * dir;
                body.velocity = dir;

                Camera.main.transform.Rotate(0, 0, -Input.GetAxis("Rotate Z") * 3);
            })
            .AddTo(this);
    }
}
