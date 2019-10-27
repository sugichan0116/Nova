using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerApplySkill : MonoBehaviour
{
    public void Apply(Skill skill)
    {
        switch(skill)
        {
            case GunSkill gun:
                //error
                return;
            case BodySkill body:
                GetComponent<Body>().ImproveHealth(body.health);
                MessageLog.Print($"HP +{body.health}");
                return;
            case SpeedSkill speed:
                GetComponent<MoveByInput>().speed += speed.speed;
                return;
            case RegeneSkill regene:
                GetComponent<RepairSystem>().AddRegenerator(regene.regene);
                return;
        }
    }
}
