using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tapeRecorderMusic : MonoBehaviour
{

    [SerializeField] private AudioSource music;
    bool isPaused;

    private void Start() {isPaused = false;}
    public void OnButtonPress()
    {
        if(isPaused)
        {
            isPaused = false;
            music.UnPause();
        }
        else music.Play();
    }

    public void PauseButton()
    {
        isPaused = true;
    }
}
