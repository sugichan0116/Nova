using BayatGames.SaveGameFree;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;
using System.Threading.Tasks;

public class SaveData : MonoBehaviour
{
    [ReadOnly]
    public string identifier;
    public FileInfo info;

    public TextMeshProUGUI title, sub, detail;
    public UnityEvent onLoad;

    // Start is called before the first frame update
    void Start()
    {
        var state = LoadManager.Instance.GameState(identifier);
        var player = (PlayerState)(state.Load("player").data);

        sub.text = info.LastWriteTime.ToString();
        detail.text = $"Gem:{player.money}";
    }

    public void Load()
    {
        SaveSlotManager.Instance.SetIdentifier(identifier);
        onLoad.Invoke();
    }

    public void ChangeProtection()
    {
        LoadManager.Instance.ChangeSaveDataProtection(this);
    }

    public void Delete()
    {
        LoadManager.Instance.DeleteSaveData(this);
    }
}
