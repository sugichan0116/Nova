using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Linq;

public class SaveSlotManager : SingletonMonoBehaviour<SaveSlotManager>
{
    public const string SAVE_DIRECTORY = "Savedata/";
    public const string DEFAULT_SLOT = "default";
    public const string EXTEND = ".data";

    public string identifier;
    public UnityEvent onSelect;

    [Header("Load")]
    public bool loadOnStart;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void Identifier(string _identifier)
    {
        identifier = _identifier;
        onSelect.Invoke();
    }

    public string SaveDataPath()
    {
        if (string.IsNullOrEmpty(identifier))
        {
            identifier = NewSaveDataName();
        }

        return CurrentSaveDataPath();
    }

    public IEnumerable<string> ReadSaveDataName()
    {
        return ReadSaveData().Select(f => GetPathWithoutExtension(f.Name));
    }

    public IEnumerable<FileInfo> ReadSaveData()
    {
        var path = $"{Application.dataPath}/{SAVE_DIRECTORY}";
        Debug.Log($"[Read] save data {path} ({this})");

        DirectoryInfo dir = new DirectoryInfo(path);
        return dir.GetFiles($"*{EXTEND}");
    }


    private string NewSaveDataName()
    {
        var names = ReadSaveDataName();
        return $"save{names.Count()}"; 
        //$"{SAVE_DIRECTORY}save{names.Count()}{EXTEND}";
    }

    private string CurrentSaveDataPath()
    {
        return SAVE_DIRECTORY + identifier + EXTEND;
    }
    
    public static string GetPathWithoutExtension(string path)
    {
        var extension = Path.GetExtension(path);
        if (string.IsNullOrEmpty(extension))
        {
            return path;
        }
        return path.Replace(extension, string.Empty);
    }
}
