using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SpeedSkill : Skill
{
    public float speed;

    public override string ToDescription()
    {
        var now = StoreManager.Instance.Dispatch(StateProps.PLAYER_MOVE_SPEED);

        return $"Gem {gem}\n" +
            $"Move_Speed +{speed}km/sec\n" +
            $"{Indent($"Now_Speed {now}km/sec")}\n" +
            $"{Indent($"Rate +{(int)(100 * speed / now)}%")}";
    }

    public override string ToTips()
    {
        return "[Passive] Speed UP";
    }
}
