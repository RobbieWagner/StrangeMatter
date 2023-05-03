using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoveCloser : GoalEffect
{
    [SerializeField] string newText;
    [SerializeField] float delayTime;
    [SerializeField] float displayTime;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] GameObject newMatter;

    public override void ActivateGoalEffect()
    {
        StartCoroutine(DisplayNewMatter());
    }

    IEnumerator DisplayNewMatter()
    {
        yield return new WaitForSeconds(delayTime);

        Instantiate(newMatter, transform);
        text.text = newText;

        text.enabled = true;

        yield return new WaitForSeconds(displayTime);

        text.enabled = false;

        StopCoroutine(DisplayNewMatter());
    }
}
