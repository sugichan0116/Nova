using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using UniRx.Triggers;

public class MinimapMarker : MonoBehaviour
{
    public float effectiveDistance = float.PositiveInfinity;

    // Start is called before the first frame update
    void Start()
    {
        var manager = MinimapManager.Instance;
        var parent = transform.parent;
        var scaleOffset = transform.localScale;

        Observable
            .EveryUpdate()
            .Where(_ => (parent.position - manager.transform.position).magnitude <= effectiveDistance)
            .Subscribe(_ =>
            {
                var position = manager.range.ClosestPoint(parent.position);
                var isOutRange = (Vector2)parent.position != position;
                var scaleRate = (isOutRange ? manager.outRangeScale : 1f);

                transform.position = position;
                transform.localScale = scaleOffset * scaleRate;
            })
            .AddTo(this);
    }
}
