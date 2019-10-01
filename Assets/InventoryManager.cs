using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : SingletonMonoBehaviour<InventoryManager>
{
    public float money;

    public void EarnMoney(float volume)
    {
        money += volume;
    }

}
