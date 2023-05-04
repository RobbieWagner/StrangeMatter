using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class CutsceneManager : DialogueManager
{

    [SerializeField] AudioSource gunCock;
    [SerializeField] AudioSource gunShoot;
    [SerializeField] Image blackoutScreen;
    [SerializeField] TextMeshProUGUI endText;

    protected override void Start()
    {
        base.Start();

        StartCoroutine(RunCutscene());
    }

    // Update is called once per frame
    protected override void Update(){  }

    public IEnumerator RunCutscene()
    {
        while(blackoutScreen.color.a > 0)
        {
            blackoutScreen.color = new Color(0,0,0,blackoutScreen.color.a - .05f);
            yield return new WaitForSeconds(.25f);
        }
        blackoutScreen.enabled = false;
        while(blackoutScreen.color.a < 1)
        {
            blackoutScreen.color = new Color(0,0,0,blackoutScreen.color.a + .05f);
            yield return new WaitForSeconds(.25f);
        }

        if(dialogueFile != null) yield return StartCoroutine(StartDialogue());

        if(PlayerPrefs.GetInt("ending") == 2)
        {
            yield return new WaitForSeconds(1.5f);
            gunCock.Play();
            yield return new WaitForSeconds(3f);
            gunShoot.Play();
            blackoutScreen.enabled = true;
            yield return new WaitForSeconds(3f);
            endText.enabled = true;
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene("MainMenu");
        }
    }

    //Sets up a dialogue to be run
    public override IEnumerator StartDialogue(Sprite displayImage = null)
    {
        //Debug.Log("starting dialogue");
        if(!dialogueRunning)
        {
            dialogueText.enabled = true;

            currentSentence = 0;
            dialogueCanvas.enabled = true;
            
            sentences = JsonUtility.FromJson<DialogueSentences>(dialogueFile.text).sentences;

            //For cutscene override
            curImage = -1;

            dialogueRunning = true;

            foreach(Sentence sentence in sentences) yield return StartCoroutine(TypeSentence(sentence, .05f, true));
        }
        EndDialogue();
        StopCoroutine(StartDialogue(displayImage));
    }


    protected override void EndDialogue()
    {
        dialogueRunning = false;
        EventSystem.current.SetSelectedGameObject(null);
        dialogueText.enabled = false;
        currentSentence = 0;
    }
}
