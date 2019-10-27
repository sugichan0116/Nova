using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class MessageLog : SingletonMonoBehaviour<MessageLog>
{
    public GameObject message;
    public UnityEvent onError;

    public static void Print(string text)
    {
        //text = ErrorText(text);
        Instance.Call(text);
    }

    private string ErrorText(string text)
    {
        if (text.Contains("Error"))
        {
            onError.Invoke();
            text = $"<color=red>{text}</color>";
        }
        return text;
    }

    public void Call(string text)
    {
        text = ErrorText(text);
        Instantiate(message, transform).GetComponentInChildren<TextMeshProUGUI>().text = text;
    }
}
