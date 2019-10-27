using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSaveDataSetter : MonoBehaviour
{
    public void SetNewSaveData()
    {
        SaveSlotManager.Instance.SetBlankIdentifier();
    }
}
