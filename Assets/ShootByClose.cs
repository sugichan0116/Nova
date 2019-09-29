using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ShootByClose : MonoBehaviour
{
    [SerializeField]
    float radius = 16f;

    // Start is called before the first frame update
    void Start()
    {
        var guns = GetComponentsInChildren<Gun>();

        Observable
            .EveryFixedUpdate()
            .Subscribe(_ => {
                var target = Player.Instance.transform.position;
                if (CanSee(target))
                {
                    foreach (var gun in guns)
                    {
                        gun.onShoot.OnNext(target - transform.position);
                    }
                }
            })
            .AddTo(this);
    }

    private bool CanSee(Vector3 target)
    {
        return (target - transform.position).magnitude < radius;
    }
}
