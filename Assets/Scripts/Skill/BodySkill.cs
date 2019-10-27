using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BodySkill : Skill
{
    public float health;

    public override string ToDescription()
    {
        return $"Gem {gem}\n" +
            $"Health(HP) +{health}";
    }

    public override string ToTips()
    {
        return "[Passive] Health UP";
    }
}
