using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

[CreateAssetMenu]
public class GunSkill : Skill
{
    public Gun gun;

    public override Sprite Icon()
    {
        Debug.Log($"[GunSkill] icon");
        return gun.Icon();
    }
}