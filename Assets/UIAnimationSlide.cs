using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIAnimation : MonoBehaviour
{
    public float during = 0.2f;
    public Ease inEase, outEase;

    public abstract float Show();
    public abstract float Hide();
}

public class UIAnimationSlide : UIAnimation
{
    [Header("Debug")]
    public Vector2 direction;
    private Vector2 screenSize;

    private bool isCashed = false;
    private Vector3 position, defaultPosition;

    public override float Show()
    {
        Init();

        transform
            .DOLocalMove(defaultPosition, during)
            .SetEase(inEase);

        return during;
    }

    public override float Hide()
    {
        Init();

        transform
            .DOLocalMove(position, during)
            .SetEase(outEase);

        return during;
    }

    private void Init()
    {
        if (!isCashed)
        {
            screenSize = new Vector2(Screen.width, Screen.height);
            defaultPosition = transform.localPosition;
            position = defaultPosition + (Vector3)(screenSize * direction);
            transform.localPosition = position;
            isCashed = true;
        }
    }
}
