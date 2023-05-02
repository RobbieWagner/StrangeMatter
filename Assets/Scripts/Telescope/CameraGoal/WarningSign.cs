using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WarningSign : MonoBehaviour
{

    [SerializeField] AudioSource warningSound;
    [SerializeField] TextMeshProUGUI warningSignText;

    // Start is called before the first frame update
    void Start()
    {
        warningSignText.enabled = false;
    }

    public void FlashWarning(string text)
    {
        warningSignText.text = text;
        warningSignText.enabled = true;
        warningSound.Play();
    }
}
