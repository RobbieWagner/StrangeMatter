using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGoal : MonoBehaviour
{

    TelescopeCamera telescopeCamera;
    [SerializeField] public GoalEffect goalEffect;
    [SerializeField] public string goalAchievementText;

    private void Start() 
    {
        telescopeCamera = GameObject.Find("CameraButton").GetComponent<TelescopeCamera>();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Camera"))
        {
            telescopeCamera.EnableCamera(this);
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Camera"))
        {
            telescopeCamera.DisableCamera();
        }
    }
}
