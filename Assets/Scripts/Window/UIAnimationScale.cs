using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIAnimationScale : UIAnimation
{
    public Vector3 startScale;

    private bool isCashed = false;
    private Vector3 defaultScale;

    public override float Show()
    {
        Init();

        transform
            .DOScale(defaultScale, during)
            .SetEase(inEase)
            .SetUpdate(true);

        return during;
    }

    public override float Hide()
    {
        Init();

        transform
            .DOScale(startScale, during)
            .SetEase(outEase)
            .SetUpdate(true);

        return during;
    }

    private void Init()
    {
        if (!isCashed)
        {
            defaultScale = transform.localScale;
            transform.localScale = startScale;
            isCashed = true;
        }
    }
}
