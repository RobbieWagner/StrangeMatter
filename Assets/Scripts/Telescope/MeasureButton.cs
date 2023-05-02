using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeasureButton : MonoBehaviour
{
    [SerializeField] AudioSource beepSound;
    [SerializeField] AudioSource reachedGoalSound;

    [SerializeField] Button measureButton;

    private MeasurementGoal foundGoal;
    [SerializeField] Level level;

    // Start is called before the first frame update
    void Start()
    {
        measureButton.interactable = false;
    }

    public void EnableRuler(MeasurementGoal measureGoal) 
    {
        measureButton.interactable = true;
        foundGoal = measureGoal;
    }
    public void DisableRuler() 
    {
        measureButton.interactable = false;
        foundGoal = null;
    }

    public void MeasureDistance()
    {
        if(!TelescopeMovement.moving)
        {
            beepSound.Play();

            if(foundGoal != null)
            {
                level.CheckOffGoal(foundGoal);
            }
        }
    }
}
