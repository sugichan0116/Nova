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

[Serializable]
[CreateAssetMenu]
public class SaveSetting : ScriptableObject
{
    [Header("Settings")]
    [SerializeField]
    private bool encode;
    [SerializeField]
    private string encodePassword;
    [SerializeField]
    private SaveFormat format = SaveFormat.JSON;
    private ISaveGameSerializer serializer;
    private ISaveGameEncoder encoder;
    private Encoding encoding;
    [SerializeField]
    private SaveGamePath savePath = SaveGamePath.PersistentDataPath;

    public string EncodePassword
    {
        get
        {
            if (string.IsNullOrEmpty(encodePassword))
            {
                encodePassword = SaveGame.EncodePassword;
            }
            return encodePassword;
        }
    }

    public ISaveGameSerializer Serializer
    {
        get
        {
            if (serializer == null)
            {
                serializer = SaveGame.Serializer;
            }

            switch (Format)
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

            return serializer;
        }
    }

    public ISaveGameEncoder Encoder
    {
        get
        {
            if (encoder == null)
            {
                encoder = SaveGame.Encoder;
            }
            return encoder;
        }
    }

    public Encoding Encoding
    {
        get
        {
            if (encoding == null)
            {
                encoding = SaveGame.DefaultEncoding;
            }
            return encoding;
        }
    }

    public bool Encode { get => encode; }
    public SaveFormat Format { get => format; }
    public SaveGamePath SavePath { get => savePath; }
}
