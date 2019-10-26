using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SaveSlotManager : SingletonMonoBehaviour<SaveSlotManager>
{
    public string identifier;
    public UnityEvent onSelect;

    // Start is called before the first frame update
    void Start()
    {
        //????
        DontDestroyOnLoad(this.gameObject);
    }

    public void Identifier(string _identifier)
    {
        identifier = _identifier;
        onSelect.Invoke();
    }
}
