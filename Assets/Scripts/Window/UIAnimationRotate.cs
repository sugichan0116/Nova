using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIAnimationRotate : UIAnimation
{
    [Header("Debug")]
    public Vector3 rotate;
    //public Quaternion roate;

    private bool isCashed = false;
    private Quaternion rotation, defaultRotation;

    public override float Show()
    {
        Init();

        transform
            .DOLocalRotate(rotate, during)
            .SetRelative()
            .SetEase(outEase)
            .SetUpdate(true);

        return during;
    }

    public override float Hide()
    {
        Init();

        transform
            .DOLocalRotate(-rotate, during)
            .SetRelative()
            .SetEase(inEase)
            .SetUpdate(true);

        return during;
    }


    private void Init()
    {
        if (!isCashed)
        {
            defaultRotation = transform.localRotation;
            rotation = defaultRotation * Quaternion.Euler(-rotate);
            transform.localRotation = rotation;
            isCashed = true;
        }
    }
}
