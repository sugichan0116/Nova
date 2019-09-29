using UnityEngine;
using UniRx;
using System;
using UniRx.Triggers;

public class LifeTime : MonoBehaviour
{
    public float lifetime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        Observable
            .Timer(TimeSpan.FromSeconds(lifetime))
            .Subscribe(_ => Destroy(gameObject))
            .AddTo(this);

    }
}
