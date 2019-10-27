using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Linq;
using System;

public class SaveSlotManager : EnvironmentMonoBehaviour<SaveSlotManager>
{
    private string identifier;
    private bool loadOnStart;

    public bool LoadOnStart { get => loadOnStart; private set => loadOnStart = value; }

    public void SetBlankIdentifier()
    {
        identifier = "";
        LoadOnStart = false;
    }

    public void SetIdentifier(string _identifier)
    {
        identifier = _identifier;
        LoadOnStart = true;
    }

    public string SaveDataPath()
    {
        if (string.IsNullOrEmpty(identifier))
        {
            identifier = NewSaveDataName();
        }

        return CurrentSaveDataPath();
    }


    private string NewSaveDataName()
    {
        var names = SaveDataReader.ReadSaveDataName();
        return $"save{DateTime.Now.ToString("yyyyMMddhhmmss")}";
        //return $"save{DateTime.Now.GetHashCode()}";
    }

    private string CurrentSaveDataPath()
    {
        return SaveDataReader.SaveDataPathFrom(identifier);
    }
    
}

public class SaveDataReader
{
    public const string SAVE_DIRECTORY = "Savedata/";
    //public const string DEFAULT_SLOT = "default";
    public const string EXTEND = ".data";

    public static string SaveDataPathFrom(string name)
    {
        return $"{SAVE_DIRECTORY}{name}{EXTEND}";
    }

    public static IEnumerable<string> ReadSaveDataName()
    {
        return ReadSaveData().Select(f => GetPathWithoutExtension(f.Name));
    }

    public static IEnumerable<FileInfo> ReadSaveData()
    {
        var path = $"{Application.dataPath}/{SAVE_DIRECTORY}";

        SafeCreateDirectory(path);
        DirectoryInfo dir = new DirectoryInfo(path);
        return dir.GetFiles($"*{EXTEND}");
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
    
    /// <summary>
     /// 指定したパスにディレクトリが存在しない場合
     /// すべてのディレクトリとサブディレクトリを作成します
     /// </summary>
    public static DirectoryInfo SafeCreateDirectory(string path)
    {
        if (Directory.Exists(path))
        {
            return null;
        }
        return Directory.CreateDirectory(path);
    }
}
