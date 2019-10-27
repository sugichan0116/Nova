using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [SerializeField]
    GameObject prefab;
    
    public void Instantiate()
    {
        var obj = Instantiate(prefab, transform.position, transform.rotation);

        Diffusion(obj);
    }

    public static void Diffusion(GameObject obj)
    {
        var rd = obj.GetComponent<Rigidbody2D>();
        if (rd == null) return;

        var rotate = Quaternion.Euler(0, 0, Random.Range(0, 360));
        rd.velocity = rotate * Vector2.up * Random.Range(0, 10);
    }
}
