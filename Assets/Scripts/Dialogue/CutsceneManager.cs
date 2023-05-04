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

    [SerializeField] TextAsset secondDialogueFile;
    [SerializeField] Canvas ending3;

    [SerializeField] bool endingCutscene = true;

    [SerializeField] bool playingRecording;
    bool canPressRecord;

    [SerializeField] TapeRecorderMusic tapeRecorder;
    [SerializeField] Sprite[] recorderSprites;
    [SerializeField] Image recorder;
    [SerializeField] Image textImage;

    protected override void Start()
    {
        base.Start();

        if(endingCutscene) StartCoroutine(RunCutscene());
        else 
        {
            playingRecording = false;
            canPressRecord = true;
            tapeRecorder.recording = false;
        }
    }

    // Update is called once per frame
    protected override void Update(){  }

    public void PlayTapeRecorder()
    {
        if(!playingRecording)StartCoroutine(PlayRecording());
        tapeRecorder.recording = false;
    }

    public void RecordButton()
    {
        if(!playingRecording && !tapeRecorder.recording)
        {
            tapeRecorder.recording = true;
            recorder.sprite = recorderSprites[1];
        }
        else if(tapeRecorder.recording)
        {
            tapeRecorder.recording = false;
            recorder.sprite = recorderSprites[0];
        }
        
    }

    public IEnumerator PlayRecording()
    {
        tapeRecorder.listenedToRecording = true;
        textImage.enabled = true;
        playingRecording = true;
        canPressRecord = false;
        recorder.sprite = recorderSprites[2];

        yield return StartCoroutine(StartDialogue());

        textImage.enabled = false;
        recorder.sprite = recorderSprites[0];
        canPressRecord = true;
        playingRecording = false;
        StopCoroutine(PlayRecording());
    }

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
            yield return new WaitForSeconds(.02f);
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
        else if(PlayerPrefs.GetInt("ending") == 3)
        {
            yield return new WaitForSeconds(1.5f);
            dialogueFile = secondDialogueFile;
            yield return StartCoroutine(StartDialogue());
            blackoutScreen.enabled = true;
            yield return new WaitForSeconds(3f);
            ending3.enabled = true;
            yield return new WaitForSeconds(4f);
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
