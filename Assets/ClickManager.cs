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

    public AudioPlayer windowAudio, repairAudio, unlockAudio;

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
                windowAudio.Play();
                machineshop.Open();
                return;
            case ClickMessage.AWAKE_REPAIR:
                windowAudio.Play();
                repair.Open();
                return;
            case ClickMessage.AWAKE_MARKET:
                windowAudio.Play();
                market.Open();
                return;
            case ClickMessage.AWAKE_DEVELOP:
                windowAudio.Play();
                develop.Open();
                return;
            case ClickMessage.AWAKE_SLOT_UNLOCK:
                windowAudio.Play();
                confirm_gunslot.Open();
                return;


            case ClickMessage.ACTION_REPAIR:
                repairAudio.Play();
                RepairSystem.Instance.Repair();
                return;
            case ClickMessage.ACTION_UNLOCK_GUNSLOT:
                unlockAudio.Play();
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
}
