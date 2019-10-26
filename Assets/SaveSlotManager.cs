using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SaveSlotManager : SingletonMonoBehaviour<SaveSlotManager>
{
    public const string SAVE_DIRECTORY = "Savedata/";
    public const string DEFAULT_SLOT = "default";

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

    public string SaveSlotPath()
    {
        if (string.IsNullOrEmpty(identifier))
        {
            return NewSaveSlot();
        }
        else
        {
            return CurrentSaveSlot();
        }
    }

    private string NewSaveSlot()
    {
        //return SAVE_DIRECTORY + $"save{SaveSlots.Count()}";

        //test gomi naose///////////////////////////////
        return SAVE_DIRECTORY + DEFAULT_SLOT;
    }

    private string CurrentSaveSlot()
    {
        return SAVE_DIRECTORY + identifier;
    }

    public IEnumerable<string> SaveSlots()
    {
        //getFiles(SAVE_DIRECTORY)
        return null;
    }
}
