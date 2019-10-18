using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu]
public class GunSkill : Skill
{
    public Gun gun;
    [Multiline]
    public string tips;

    public override Sprite Icon()
    {
        //Debug.Log($"[GunSkill] icon");
        return gun.Icon();
    }

    public override string ToDescription()
    {
        //＋どこまで飛ぶかの項目
        var bullet = gun.Prefab;
        var time = bullet.GetComponent<LifeTime>().lifetime;
        var fps = 1000f / (gun.frame * 100f); // fire / sec
        var dps = bullet.damage * fps; //damage / sec

        return $"Gem x{gem}\n" +
            $"Damage {(int)(dps * 60f)}-/min\n" +
            $"{Indent($"Damage {bullet.damage}")}\n" +
            $"{Indent($"Firing {60f * fps}-/min")}\n" +
            $"Speed {gun.speed}-km/sec\n" +
            $"Range {gun.speed * time}-km\n";
    }

    public override string ToTips()
    {
        return ((tips != "") ? $"{tips}\n" : "") +
            "<size=80%>[Unlock] >> [Equip]</size>";
    }
}