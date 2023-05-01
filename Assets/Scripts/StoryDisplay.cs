using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class StoryDisplay : MonoBehaviour
{

    [SerializeField] Canvas canvas;
    [SerializeField] AudioMixer audioMixer;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        canvas.enabled = true;
        audioMixer.SetFloat("music", 0);
        audioMixer.SetFloat("game", 0);
        audioMixer.SetFloat("volume", PlayerPrefs.GetFloat("main_volume"));
        audioMixer.SetFloat("ui", PlayerPrefs.GetFloat("ui_volume"));

        PauseMenu.paused = true;  
    }

    public void OnPlayButton()
    {
        Time.timeScale = 1f;
        canvas.enabled = false;
        
        audioMixer.SetFloat("music", PlayerPrefs.GetFloat("music_volume"));
        audioMixer.SetFloat("game", PlayerPrefs.GetFloat("ghost_volume"));

        PauseMenu.paused = false;  
    }
}
