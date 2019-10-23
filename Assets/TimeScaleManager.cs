using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaleManager : MonoBehaviour
{
    public void Pause()
    {
        Debug.Log($"[Time Scale] pause");
        Time.timeScale = 0;
    }

    public void Restart()
    {
        Debug.Log($"[Time Scale] restart");
        Time.timeScale = 1.0f;
    }
}
