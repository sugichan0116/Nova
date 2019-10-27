using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;
using BayatGames.SaveGameFree;
using System.Threading.Tasks;

public class LoadManager : SingletonMonoBehaviour<LoadManager>
{
    public SaveSetting setting;

    [Header("Prefabs")]
    public SaveData prefab;
    public GameObject noData;

    // Start is called before the first frame update
    void Start()
    {
        ReadSaveData();
    }

    public void ReadSaveData()
    {
        var info = SaveDataReader.ReadSaveData();

        ResetSaveData();
        if (info.Count() == 0)
        {
            Instantiate(noData, transform);
        }

        foreach (var file in info)
        {
            Debug.Log($"[SaveData] {file}");
            var savedata = Instantiate(prefab, transform);
            var filename = SaveDataReader.GetPathWithoutExtension(file.Name);
            savedata.identifier = filename;
            savedata.info = file;
        }
    }

    public GameState GameState(string identifier)
    {
        var path = SaveDataReader.SaveDataPathFrom(identifier);

        Debug.Log($"[MetaData] loading... {path}");

        return SaveGame.Load<GameState>(
            path,
            new GameState(),
            setting.Encode,
            setting.EncodePassword,
            setting.Serializer,
            setting.Encoder,
            setting.Encoding,
            setting.SavePath);
    }

    public void ChangeSaveDataProtection(SaveData saveData)
    {

    }

    public void DeleteSaveData(SaveData saveData)
    {
        Debug.Log($"[SaveData] delete {saveData.identifier}");
        saveData.info.Delete();

        ReadSaveData();
    }

    private void ResetSaveData()
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
