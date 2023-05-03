using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Kidnapping : GoalEffect
{

    [SerializeField] AudioSource footstepSounds;
    [SerializeField] AudioSource sackSound;

    [SerializeField] GameObject sack;

    public override void ActivateGoalEffect()
    {
        StartCoroutine(KidnapPlayer());
    }

    IEnumerator KidnapPlayer()
    {
        footstepSounds.Play();
        while(footstepSounds.isPlaying) yield return null;
        if(sackSound != null) sackSound.Play();

        sack.SetActive(true);
        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene("MainMenu");
    }
}
