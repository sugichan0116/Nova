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
    public const string SAVE_DIRECTORY = "Savedata/";
    public const string DEFAULT_SLOT = "default";

    [HideInInspector]
    public string identifier;
    public bool encode;
    public string encodePassword;
    public SaveFormat format = SaveFormat.JSON;
    public ISaveGameSerializer serializer;
    public ISaveGameEncoder encoder;
    public Encoding encoding;
    public SaveGamePath savePath = SaveGamePath.PersistentDataPath;

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

    public Subject<Unit> onSave = new Subject<Unit>();
    public Subject<Unit> onLoad = new Subject<Unit>();
    public Subject<Unit> onSaved = new Subject<Unit>();

    protected virtual void Awake()
    {
        if (string.IsNullOrEmpty(encodePassword))
        {
            encodePassword = SaveGame.EncodePassword;
        }
        if (serializer == null)
        {
            serializer = SaveGame.Serializer;
        }
        if (encoder == null)
        {
            encoder = SaveGame.Encoder;
        }
        if (encoding == null)
        {
            encoding = SaveGame.DefaultEncoding;
        }

        switch (format)
        {
            case SaveFormat.Binary:
                serializer = new SaveGameBinarySerializer();
                break;
            case SaveFormat.JSON:
                serializer = new SaveGameJsonSerializer();
                break;
            case SaveFormat.XML:
                serializer = new SaveGameXmlSerializer();
                break;
        }
    }

    private void UpdateIdentifier()
    {
        var slot = SaveSlotManager.Instance;
        if(slot != null)
        {
            if(string.IsNullOrEmpty(slot.identifier))
            {
                identifier = SAVE_DIRECTORY + DEFAULT_SLOT;
            }
            else
            {
                identifier = SAVE_DIRECTORY + slot.identifier;
            }
        }
        else
        {
            Debug.LogWarning($"[Save slot] can't load");
        }
    }

    public void ExecuteSave()
    {
        Debug.Log($"[execute] save");

        UpdateIdentifier();
        GameState = new GameState();

        onSaved
            //.Do(_ => Debug.Log($"[s]{_}"))
            .Throttle(TimeSpan.FromSeconds(0.1f))
            .Take(1)
            .Subscribe(_ =>
            {
                Debug.Log("[Manager] saved");
                SaveGame.Save<GameState>(
                    identifier,
                    GameState,
                    encode,
                    encodePassword,
                    serializer,
                    encoder,
                    encoding,
                    savePath);
            })
            .AddTo(this);

        onSave.OnNext(Unit.Default);

    }

    public void ExecuteLoad()
    {
        Debug.Log($"[execute] load");

        UpdateIdentifier();
        GameState = SaveGame.Load<GameState>(
                     identifier,
                     new GameState(),
                     encode,
                     encodePassword,
                     serializer,
                     encoder,
                     encoding,
                     savePath);

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
        return book[key];
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