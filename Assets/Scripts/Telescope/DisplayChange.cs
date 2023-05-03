using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayChange : MonoBehaviour
{

    [SerializeField] private Sprite screenImage;
    [SerializeField] private Image screen;

    bool updatedScreen;

    // Start is called before the first frame update
    void Start()
    {
        updatedScreen = false;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Camera") && !updatedScreen)
        {
            updatedScreen = true;
            screen.enabled = true;
            screen.sprite = screenImage;
        }
    }
}
