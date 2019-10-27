using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;

public class SaveSlotReader : MonoBehaviour
{
    public SaveData prefab;

    // Start is called before the first frame update
    void Start()
    {
        ReadSaveData();
    }

    public void ReadSaveData()
    {
        var info = SaveDataReader.ReadSaveData();

        if (info.Count() == 0) return;

        ResetSaveData();
        foreach (var file in info)
        {
            Debug.Log($"[SaveData] {file}");
            var savedata = Instantiate(prefab, transform);
            var filename = SaveDataReader.GetPathWithoutExtension(file.Name);
            savedata.identifier = filename;
            savedata.info = file;
        }
    }

    private void ResetSaveData()
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
