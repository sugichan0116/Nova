using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLocator : MonoBehaviour
{
    public Vector2 offset;

    // Update is called once per frame
    void Update()
    {
        transform.position = Input.mousePosition + (Vector3)offset;
    }
}
