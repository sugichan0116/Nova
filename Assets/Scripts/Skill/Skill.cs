using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Skill : ScriptableObject
{
    public int skillID;
    public string skillName;
    public float gem;
    public Sprite icon;

    public virtual Sprite Icon()
    {
        return icon;
    }

    public virtual string ToDescription()
    {
        return "this is skill description\n" +
            "\n" +
            $"Gem : {gem}";
    }

    public virtual string ToTips()
    {
        return "[Tips]";
    }

    public string Indent(string text)
    {
        return $"<size=80%>--{text}</size>";
    }
}
