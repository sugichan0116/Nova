using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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