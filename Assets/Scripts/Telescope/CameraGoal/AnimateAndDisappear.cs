using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateAndDisappear : GoalEffect
{
    [SerializeField] Animator animator;
    [SerializeField] float timeToWait = .5f;

    public override void ActivateGoalEffect()
    {
        if(timeToWait > 0) StartCoroutine(WaitToDestroy());
        else Destroy(gameObject);
    }

    IEnumerator WaitToDestroy()
    {
        animator.SetBool("animate", true);
        yield return new WaitForSeconds(timeToWait);

        Destroy(gameObject);
    }
}
