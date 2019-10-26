using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class PlayerState
{
    public float money;
}

public class SavePlayerState : SavePort
{
    protected override PackageObject Save()
    {
        var state = new PlayerState();
        state.money = InventoryManager.Instance.Money;

        return new PackageObject(typeof(PlayerState), state);
    }
    protected override void Load(PackageObject package)
    {
        var state = (PlayerState)(package.data);
        InventoryManager.Instance.Money = state.money;
    }
}
