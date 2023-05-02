using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CheckListObject : MonoBehaviour
{
    [SerializeField] private Image checkBox;

    // Start is called before the first frame update
    void Start()
    {
        checkBox.enabled = false;
    }

    public void CheckBox()
    {
        checkBox.enabled = true;
    }
}
