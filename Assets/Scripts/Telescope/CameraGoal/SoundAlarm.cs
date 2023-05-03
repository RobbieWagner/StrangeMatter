using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundAlarm : GoalEffect
{
    [SerializeField] GameObject toTurnOn;
    [SerializeField] WarningSign warningSign;
    [SerializeField] string warningSignText;

    [SerializeField] bool waitToActivate = true;
    [SerializeField] float timeToWait = 3f;

    [SerializeField] AudioSource alarm;
    [SerializeField] Image redFlash;
    [HideInInspector] public bool flashing;

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
        StartCoroutine(PoundAlarm());

        StopCoroutine(WaitToActivate());
    }

    IEnumerator PoundAlarm()
    {
        yield return new WaitForSeconds(1f);
        flashing = true;
        redFlash.color = new Color(1,0,0,0f);
        redFlash.enabled = true;
        alarm.Play();

        while(flashing)
        {
            while(redFlash.color.a < .5f)
            {
                redFlash.color = new Color(1,0,0,redFlash.color.a + 0.1f);
                yield return new WaitForSeconds(.1f);
            }
            yield return new WaitForSeconds(2f);
            while(redFlash.color.a > 0)
            {
                redFlash.color = new Color(1,0,0,redFlash.color.a - 0.1f);
                yield return new WaitForSeconds(.1f);
            }
            yield return new WaitForSeconds(2f);
        }
    }
}
