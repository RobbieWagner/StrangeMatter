using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnObject : GoalEffect
{
    [SerializeField] GameObject toTurnOn;
    [SerializeField] TapeRecorderMusic tapeRecorder;
    [HideInInspector] public bool canSeePhotoDataButton;

    private void Start() 
    {
        canSeePhotoDataButton = false;
    }

    public override void ActivateGoalEffect()
    {
        canSeePhotoDataButton = true;
    }

    private void Update() 
    {
        if(canSeePhotoDataButton && tapeRecorder.listenedToRecording && !toTurnOn.activeSelf)
        toTurnOn.SetActive(true);
    }
}
