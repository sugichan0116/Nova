using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreManager : SingletonMonoBehaviour<StoreManager>
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public float Get(StateProps state)
    {
        switch(state)
        {
            case StateProps.MONEY_HAVING:
                return InventoryManager.Instance.Money;
            case StateProps.MONEY_TO_REPAIR:
                return RepairSystem.Instance.NeedMoneyToRepair();
            case StateProps.MONEY_TO_UNLOCK_GUNSLOT:
                return GunManager.Instance.selectedSlot.needGem;

            case StateProps.PLAYER_MOVE_SPEED:
                return Player.Instance.GetComponent<MoveByInput>().speed;

            //case StateProps.MARKET_NAME:
            //    return MarketField.Instance.market.marketName;
            case StateProps.MARKET_TAX:
                return (int)MarketField.Instance.market.tax;
            case StateProps.MARKET_DEAL_WITH:
                return MarketField.Instance.dealWith;

            default:
                return 0;
        }
    }
}
