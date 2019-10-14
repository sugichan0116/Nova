using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : SingletonMonoBehaviour<InventoryManager>
{
    public float money;

    public List<Gun> guns;

    public void EarnMoney(float volume)
    {
        money += volume;
    }

    public bool TryToPay(float volume)
    {
        if(money >= volume)
        {
            money -= volume;
            return true;
        }
        return false;
    }

}
