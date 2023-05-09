using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnimateAndDisappear : GoalEffect
{
    [SerializeField] Animator animator;
    [SerializeField] float timeToWait = .5f;

    [SerializeField] Night3SaveData saveData;
    [SerializeField] TapeRecorderMusic tapeRecorder;
    [SerializeField] TextMeshProUGUI text;

    [SerializeField] AudioSource sound;

    public override void ActivateGoalEffect()
    {
        if(timeToWait > 0) StartCoroutine(WaitToDestroy());
        else Destroy(gameObject);

        text.enabled = false;
    }

    IEnumerator WaitToDestroy()
    {
        animator.SetBool("animate", true);
        yield return new WaitForSeconds(timeToWait);

        if(tapeRecorder.recording) saveData.recordedMonster = true;
        sound.Play();
        
        Destroy(gameObject);
    }
}
