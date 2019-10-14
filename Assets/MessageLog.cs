using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessageLog : SingletonMonoBehaviour<MessageLog>
{
    public GameObject message;

    public static void Print(string text)
    {
        Instance.Call(text);
    }

    private void Call(string text)
    {
        Instantiate(message, transform).GetComponentInChildren<TextMeshProUGUI>().text = text;
    }
}
