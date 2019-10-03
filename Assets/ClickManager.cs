using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : SingletonMonoBehaviour<ClickManager>
{
    public Window machineshop;
    public Window repair;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ListenMessage(ClickMessage message)
    {
        switch(message)
        {
            case ClickMessage.AWAKE_MACHINESHOP:
                machineshop.Open();
                return;
            case ClickMessage.AWAKE_REPAIR:
                repair.Open();
                return;
            case ClickMessage.ACTION_REPAIR:
                InventoryManager.Instance.money -= 300;
                Player.Instance.Body.RepairDamage(500);
                return;
            default:

                return;
        }
    }
}
