using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreManager : SingletonMonoBehaviour<StoreManager>
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public float Dispatch(StateProps state)
    {
        switch(state)
        {
            case StateProps.MONEY_HAVING:
                return InventoryManager.Instance.money;
            case StateProps.MONEY_TO_REPAIR:
                return (int)(Player.Instance.Body.LostHealth() * 2.5f);
            case StateProps.MONEY_TO_UNLOCK_GUNSLOT:
                return GunManager.Instance.selectedSlot.needGem;
            default:
                return 0;
        }
    }
}
