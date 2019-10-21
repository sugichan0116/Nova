using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateForTarget : MonoBehaviour
{
    private Bullet bullet;
    public float rotateSpeed = 0.05f;
    
    // Update is called once per frame
    void Update()
    {
        bullet = bullet ?? GetComponent<Bullet>();
        var target = bullet.target;
        var body = bullet.body;
        var r = Vector3.Cross(target.transform.position - transform.position, body.velocity).z;
        var rate = (1f + body.velocity.magnitude / 10f);
        body.velocity = Quaternion.Euler(0, 0, -rotateSpeed * r * rate) * body.velocity;
    }
}
