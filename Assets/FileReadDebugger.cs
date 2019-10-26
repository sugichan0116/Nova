using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class FileReadDebugger : MonoBehaviour
{
    public string directory = "/Savedata";
    public string target = "*";

    void Start()
    {
        var text = GetComponent<TextMeshProUGUI>();

        DirectoryInfo dir = new DirectoryInfo(Application.dataPath + directory);
        FileInfo[] info = dir.GetFiles(target);
        foreach (FileInfo f in info)
        {
            text.text += $"{f.FullName}\n";
            //Debug.Log(f.Name);
        }
    }
}
