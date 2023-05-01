using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavArrow : MonoBehaviour
{
    [SerializeField] Image image;

    [SerializeField] Sprite keyUp;
    [SerializeField] Sprite keyDown;

    // Start is called before the first frame update
    void Start()
    {
        image.sprite = keyUp;
    }

    public void ChangeArrowPress()
    {
        if(!TelescopeMovement.moving)
        {
            image.sprite = keyDown;
        }
    }

    public void ResetArrow()
    {
        image.sprite = keyUp;
    }

}
