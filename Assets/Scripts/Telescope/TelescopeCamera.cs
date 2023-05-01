using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TelescopeCamera : MonoBehaviour
{
    [SerializeField] AudioSource pictureSound;
    [SerializeField] AudioSource reachedGoalSound;

    [SerializeField] Button cameraButton;

    private CameraGoal foundGoal;
    [SerializeField] Level level;

    // Start is called before the first frame update
    void Start()
    {
        cameraButton.interactable = false;
    }

    public void EnableCamera(CameraGoal cameraGoal) 
    {
        cameraButton.interactable = true;
        reachedGoalSound.Play();
        foundGoal = cameraGoal;
    }
    public void DisableCamera() 
    {
        cameraButton.interactable = false;
        foundGoal = null;
    }

    public void TakePicture()
    {
        if(!TelescopeMovement.moving)
        {
            pictureSound.Play();
            reachedGoalSound.Play();

            if(foundGoal != null)
            {
                level.CheckOffGoal(foundGoal);
            }
        }
    }
}
