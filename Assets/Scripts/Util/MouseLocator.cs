using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLocator : MonoBehaviour
{
    public Vector2 offset;

    private void OnEnable()
    {
        SetPosition();
    }

    // Update is called once per frame
    void Update()
    {
        SetPosition();
    }

    private void SetPosition()
    {
        transform.position = Input.mousePosition + (Vector3)offset;
    }
}
