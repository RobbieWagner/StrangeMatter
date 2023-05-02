using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectMotion : GoalEffect
{

    [SerializeField] GameObject toTurnOn;
    [SerializeField] WarningSign warningSign;
    [SerializeField] string warningSignText;

    [SerializeField] bool waitToActivate = true;
    [SerializeField] float timeToWait = 3f;

    public override void ActivateGoalEffect()
    {
        if(waitToActivate) StartCoroutine(WaitToActivate());
        else
        {
            toTurnOn.SetActive(true);
            warningSign.FlashWarning(warningSignText);
        }
    }

    IEnumerator WaitToActivate()
    {
        yield return new WaitForSeconds(timeToWait);

        toTurnOn.SetActive(true);
        warningSign.FlashWarning(warningSignText);
    }
}
