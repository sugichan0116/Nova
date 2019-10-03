using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : SingletonMonoBehaviour<Player>
{
    private Body body;

    public Body Body
    {
        get
        {
            body = body ?? GetComponent<Body>();

            return body;
        }
    }
}
