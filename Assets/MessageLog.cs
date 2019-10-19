using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessageLog : SingletonMonoBehaviour<MessageLog>
{
    public GameObject message;

    public static void Print(string text)
    {
        text = ErrorText(text);
        Instance.Call(text);
    }

    private static string ErrorText(string text)
    {
        if (text.Contains("Error")) text = $"<color=red>{text}</color>";
        return text;
    }

    private void Call(string text)
    {
        Instantiate(message, transform).GetComponentInChildren<TextMeshProUGUI>().text = text;
    }
}
