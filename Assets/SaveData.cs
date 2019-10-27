using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class SaveData : MonoBehaviour
{
    public string identifier;
    public FileInfo info;

    public TextMeshProUGUI title, sub;
    public UnityEvent onLoad;

    // Start is called before the first frame update
    void Start()
    {
        title.text = identifier;
        sub.text = info.LastWriteTime.ToString();
    }

    public void Load()
    {
        SaveSlotManager.Instance.SetIdentifier(identifier);
        onLoad.Invoke();
    }

    public void ChangeProtection()
    {

    }

    public void Delete()
    {

    }
}
