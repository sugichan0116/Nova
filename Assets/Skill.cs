using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Skill : ScriptableObject
{
    public string skillName;
    public float gem;
    public Sprite icon;

    public virtual Sprite Icon()
    {
        return icon;
    }
}
