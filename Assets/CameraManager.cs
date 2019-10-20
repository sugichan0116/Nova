using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    //public Camera camera;
    public float during;

    public void CameraSize(float size)
    {
        DOTween.To(
            () => Camera.main.orthographicSize,
            value => Camera.main.orthographicSize = value,
            size,
            during
            );
        //Camera.main.orthographicSize = size;
    }
}
