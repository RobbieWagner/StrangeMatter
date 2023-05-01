using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TelescopeCamera : MonoBehaviour
{
    [SerializeField] AudioSource pictureSound;
    [SerializeField] AudioSource reachedGoalSound;

    [SerializeField] Button cameraButton;

    // Start is called before the first frame update
    void Start()
    {
        cameraButton.interactable = false;
    }

    public void EnableCamera() 
    {
        cameraButton.interactable = true;
        reachedGoalSound.Play();
    }
    public void DisableCamera() {cameraButton.interactable = false;}

    public void TakePicture()
    {
        if(!TelescopeMovement.moving)
        {
            pictureSound.Play();
            reachedGoalSound.Play();
        }
    }
}
