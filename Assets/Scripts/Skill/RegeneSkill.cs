using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RegeneSkill : Skill
{
    public float regene;
    public override string ToDescription()
    {
        return $"Gem x{gem}\n" +
            $"Regeneration(HP Repair) +{regene}/sec\n" +
            $"{Indent($" (+{regene * 60f}/min)")}\n";
    }

    public override string ToTips()
    {
        return "[Passive] Regenerator\nAuto Health Repair System";
    }
}
