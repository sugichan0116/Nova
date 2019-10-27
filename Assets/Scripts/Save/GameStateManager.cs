using BayatGames.SaveGameFree;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UniRx;
using BayatGames.SaveGameFree.Serializers;
using BayatGames.SaveGameFree.Encoders;
using System.Text;
using static BayatGames.SaveGameFree.SaveGameAuto;

public class GameStateManager : SingletonMonoBehaviour<GameStateManager>
{
    [HideInInspector]
    public string identifier;

    public SaveSetting setting;

    private GameState gameState;

    public GameState GameState
    {
        get
        {
            if (gameState == null)
            {
                gameState = new GameState();
            }
            return gameState;
        }
        private set => gameState = value;
    }

    public UnityEvent onAutoLoad;

    public Subject<Unit> onSave = new Subject<Unit>();
    public Subject<Unit> onLoad = new Subject<Unit>();
    public Subject<Unit> onSaved = new Subject<Unit>();

    private void Start()
    {
        if(SaveSlotManager.Instance.LoadOnStart)
        {
            ExecuteLoad();
            onAutoLoad.Invoke();
        }
    }

    private void UpdateIdentifier()
    {
        var manager = SaveSlotManager.Instance;

        if (manager == null)
        {
            Debug.LogWarning($"[Save slot] can't load");
            return;
        }

        identifier = manager.SaveDataPath();
    }

    public void ExecuteSave()
    {
        UpdateIdentifier();
        Debug.Log($"[execute] save {identifier}");

        GameState = new GameState();

        onSaved
            .Throttle(TimeSpan.FromSeconds(0.1f))
            .Take(1)
            .Subscribe(_ =>
            {
                Debug.Log("[Manager] saved");
                SaveGame.Save<GameState>(
                    identifier,
                    GameState,
                    setting.Encode,
                    setting.EncodePassword,
                    setting.Serializer,
                    setting.Encoder,
                    setting.Encoding,
                    setting.SavePath);
            })
            .AddTo(this);

        onSave.OnNext(Unit.Default);

    }

    public void ExecuteLoad()
    {
        UpdateIdentifier();
        Debug.Log($"[execute] load {identifier}");

        GameState = SaveGame.Load<GameState>(
                    identifier,
                    new GameState(),
                    setting.Encode,
                    setting.EncodePassword,
                    setting.Serializer,
                    setting.Encoder,
                    setting.Encoding,
                    setting.SavePath);

        onLoad.OnNext(Unit.Default);
        Debug.Log("[Manager] loaded");
    }


    public void Save(string key, PackageObject obj)
    {
        GameState.Save(key, obj);
        onSaved.OnNext(Unit.Default);
    }

    public PackageObject Load(string key)
    {
        return GameState.Load(key);
    }
}

[System.Serializable]
public class GameState
{
    [SerializeField]
    private Dictionary<string, PackageObject> book;

    public GameState()
    {
        book = new Dictionary<string, PackageObject>();
    }

    public void Save(string key, PackageObject obj)
    {
        book.Add(key, obj);
    }

    public PackageObject Load(string key)
    {
        if(book.ContainsKey(key))
        {
            return book[key];
        }

        return null;
    }
}

[System.Serializable]
public class PackageObject
{
    [SerializeField]
    public Type type; //これいらない？
    [SerializeField]
    public object data;

    public PackageObject(Type _type, object _data)
    {
        type = _type;
        data = _data;
    }
}