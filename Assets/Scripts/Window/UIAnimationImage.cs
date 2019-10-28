using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIAnimationImage : UIAnimation
{
    private bool isCashed = false;
    private Image image;

    private Color color, defaultColor;

    public override float Show()
    {
        Init();

        image
            .DOColor(defaultColor, during)
            .SetEase(inEase)
            .SetUpdate(true);

        return during;
    }

    public override float Hide()
    {
        Init();

        image
            .DOColor(color, during)
            .SetEase(outEase)
            .SetUpdate(true);

        return during;
    }

    private void Init()
    {
        if (!isCashed)
        {
            image = GetComponent<Image>();
            defaultColor = image.color;
            color = Color.clear;
            image.color = color;
            isCashed = true;
        }
    }
}
