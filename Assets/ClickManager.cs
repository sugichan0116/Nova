using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : SingletonMonoBehaviour<ClickManager>
{
    public Window machineshop;
    public Window repair;
    public Window market;
    public Window develop;
    public Window confirm_gunslot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ListenMessage(ClickMessage message)
    {
        Debug.Log($"[Dispatch] {message}");

        switch(message)
        {
            case ClickMessage.AWAKE_MACHINESHOP:
                machineshop.Open();
                return;
            case ClickMessage.AWAKE_REPAIR:
                repair.Open();
                return;
            case ClickMessage.AWAKE_MARKET:
                market.Open();
                return;
            case ClickMessage.AWAKE_DEVELOP:
                develop.Open();
                return;
            case ClickMessage.AWAKE_SLOT_UNLOCK:
                confirm_gunslot.Open();
                return;


            case ClickMessage.ACTION_REPAIR:
                Repair();
                return;
            case ClickMessage.ACTION_UNLOCK_GUNSLOT:
                GunManager.Instance.UnlockSlot();
                return;

            case ClickMessage.ACTION_MARKET_DEAL_1:
                MarketField.Instance.dealWith = 1;
                return;
            case ClickMessage.ACTION_MARKET_DEAL_10:
                MarketField.Instance.dealWith = 10;
                return;
            case ClickMessage.ACTION_MARKET_DEAL_100:
                MarketField.Instance.dealWith = 100;
                return;
            case ClickMessage.ACTION_MARKET_DEAL_1000:
                MarketField.Instance.dealWith = 1000;
                return;

            default:
                return;
        }
    }

    //test gomi kuzu
    public void Repair()
    {
        var body = Player.Instance.Body;
        var needmoney = StoreManager.Instance.Get(StateProps.MONEY_TO_REPAIR);
        var inventory = InventoryManager.Instance;

        if (inventory.TryToPay(needmoney))
        {
            body.RepairDamage(body.LostHealth());
        }
        else MessageLog.Print("[Error] Lack of Gem...");
    }
}
