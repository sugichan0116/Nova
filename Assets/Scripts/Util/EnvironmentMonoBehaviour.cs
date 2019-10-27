using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnvironmentMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                Type t = typeof(T);

                instance = (T)FindObjectOfType(t);
                if (instance == null)
                {
                    Debug.LogWarning(t + " を生成しました");
                    var environment = new GameObject(typeof(T).FullName);
                    environment.AddComponent<T>();

                    instance = environment.GetComponent<T>();
                }
            }

            return instance;
        }
    }

    virtual protected void Awake()
    {
        // 他のGameObjectにアタッチされているか調べる.
        // アタッチされている場合は破棄する.
        if (this != Instance)
        {
            Destroy(this.gameObject);
            Debug.LogWarning(
                typeof(T) +
                " は既に他のGameObjectにアタッチされているため、コンポーネントを破棄しました." +
                " アタッチされているGameObjectは " + Instance.gameObject.name + " です.");
            return;
        }

        DontDestroyOnLoad(this.gameObject);
    }

}