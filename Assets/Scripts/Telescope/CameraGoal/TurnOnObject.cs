using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnObject : GoalEffect
{
    [SerializeField] GameObject toTurnOn;

    public override void ActivateGoalEffect()
    {
        toTurnOn.SetActive(true);
    }
}
