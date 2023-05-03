using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EjectButton : MonoBehaviour
{

    [SerializeField] Image button;
    [SerializeField] Sprite pressedButton;
    [SerializeField] Animator floppyDiskSlot;
    [SerializeField] Night3SaveData saveData;

    public void EjectPress()
    {
        saveData.removedDisk = true;
        button.sprite = pressedButton;
        floppyDiskSlot.SetBool("animate", true);
    }
}
