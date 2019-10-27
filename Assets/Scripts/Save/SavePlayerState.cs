using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerState
{
    public float money;
    public float health;
    public float speed;
    public float regene;
}

public class SavePlayerState : SavePort
{
    protected override PackageObject Save()
    {
        var state = new PlayerState();
        var player = Player.Instance;
        state.money = InventoryManager.Instance.Money;
        state.health = player.Body.Health;
        state.speed = player.GetComponent<MoveByInput>().speed;
        state.regene = player.GetComponent<RepairSystem>().RegeneratorPerSecond;

        return new PackageObject(typeof(PlayerState), state);
    }

    protected override void Load(PackageObject package)
    {
        var state = (PlayerState)(package.data);
        var player = Player.Instance;
        InventoryManager.Instance.Money = state.money;
        player.Body.Health = state.health;
        player.GetComponent<MoveByInput>().speed = state.speed;
        player.GetComponent<RepairSystem>().RegeneratorPerSecond = state.regene;
    }
}
