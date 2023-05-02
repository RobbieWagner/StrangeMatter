using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectMotion : GoalEffect
{

    [SerializeField] GameObject toTurnOn;
    [SerializeField] WarningSign warningSign;
    [SerializeField] string warningSignText;

    public override void ActivateGoalEffect()
    {
        toTurnOn.SetActive(true);
        warningSign.FlashWarning(warningSignText);
    }
}
