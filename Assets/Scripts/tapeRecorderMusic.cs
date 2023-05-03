using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapeRecorderMusic : MonoBehaviour
{

    [SerializeField] private AudioSource music;
    bool isPaused;

    [HideInInspector] public bool recording;

    private void Start() 
    {
        isPaused = false;
        recording = false;
    }

    public void OnButtonPress()
    {
        if(isPaused)
        {
            isPaused = false;
            music.UnPause();
        }
        else music.Play();
        recording = false;
    }

    public void StopButton()
    {
        isPaused = false;
    }

    public void PauseButton()
    {
        recording = false;
        if(music.isPlaying)isPaused = true;
    }

    public void RecordButton()
    {
        recording = true;
        Debug.Log("recording");
    }
}
