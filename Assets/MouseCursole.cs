using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursole : SingletonMonoBehaviour<MouseCursole>
{
    public Vector3 position;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //var pos = Camera.main.WorldToScreenPoint(transform.position);
        //var rotation = Quaternion.LookRotation(Vector3.forward, Input.mousePosition - pos);
        //transform.localRotation = rotation;
        position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        position.z = 0;
        transform.position = position;
    }
}
