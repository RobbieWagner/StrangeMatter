using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;

public class DialogueManager : MonoBehaviour
{
     //DialogueSentences class stores a list of sentences and possible choices
    [System.Serializable]
    public class DialogueSentences
    {
        public List<Sentence> sentences;
    }

    //Sentence class holds properties for the person speaking, text, and choices
    [System.Serializable]
    public class Sentence
    {
        public int textID;
        public string text;
        public bool displayImage;
        public string textColor;

        //for cutscene overload
        public bool nextImage;
    }

    [SerializeField] protected List<Sentence> sentences;

    [SerializeField] public TextAsset dialogueFile;
    private Sentence sentence;
    private int curChoiceIndex;

    public TextMeshProUGUI dialogueText;

    public Canvas dialogueCanvas;
    [SerializeField] private Image spriteOnLeft;
    [SerializeField] private Image spriteOnRight;

    protected int currentSentence;

    [SerializeField] private Image blinkIcon;
    [SerializeField] private GameObject[] choicePanelsInactive;
    [SerializeField] private TextMeshProUGUI[] choiceTextsInactive;
    [SerializeField] private GameObject[] choicePanelsActive;
    [SerializeField] private TextMeshProUGUI[] choiceTextsActive;


    private bool typingSentence;
    private bool skipSentence;

    protected bool canMoveOn;
    private bool waitingForPlayerToContinue;

    int nextSentence;

    [HideInInspector]
    public bool dialogueRunning;

    [SerializeField] private Image backgroundImageDisplay;
    private Sprite backgroundImage;

    //For cutscene override
    protected int curImage;

    protected virtual void Start()
    {
        currentSentence = -1;

        if(blinkIcon != null)blinkIcon.enabled = false;

        canMoveOn = false;
        waitingForPlayerToContinue = false;

        nextSentence = 0;
        curChoiceIndex = 0;

        skipSentence = false;
        typingSentence = false;

        //For cutscene override
        curImage = -1;

        if(backgroundImageDisplay != null) backgroundImageDisplay.enabled = false;
        DeactivateChoicePanels();
    }

    protected virtual void Update(){}

    //Sets up a dialogue to be run
    public virtual IEnumerator StartDialogue(Sprite displayImage = null) {yield return null;}

    protected virtual void EndDialogue()
    {
        dialogueRunning = false;
        dialogueCanvas.enabled = false;
        EventSystem.current.SetSelectedGameObject(null);
        currentSentence = -1;
        backgroundImageDisplay.enabled = false;
        spriteOnLeft.sprite = null;
        spriteOnRight.sprite = null;
        spriteOnLeft.enabled = false;
        spriteOnRight.enabled = false;

        DeactivateChoicePanels();
    }

    //Types a sentence out into the text box
    protected IEnumerator TypeSentence(Sentence sentence, float timeBetweenCharacters = .025f, bool pauseAfter = false)
    {
        yield return null;
        curChoiceIndex = 0;
        dialogueText.text = "";
        string textToDisplay = sentence.text;
        if(sentence.textColor == null || sentence.textColor.ToLower().Equals("white")) dialogueText.color = Color.white;
        else if(sentence.textColor.ToLower().Equals("red")) dialogueText.color = Color.red;
        else if(sentence.textColor.ToLower().Equals("blue")) dialogueText.color = Color.blue; 
        else if(sentence.textColor.ToLower().Equals("green")) dialogueText.color = Color.green; 
        typingSentence = true;

        if(backgroundImageDisplay != null)
        {
            if(backgroundImageDisplay.sprite != null && sentence.displayImage && !backgroundImageDisplay.enabled)
            {
                yield return StartCoroutine(FadeInBackgroundImage(backgroundImageDisplay));
            }
            else if(backgroundImageDisplay.enabled && !sentence.displayImage) yield return StartCoroutine(FadeOutBackgroundImage(backgroundImageDisplay));
        }

        for(int i = 0; i < textToDisplay.Length; i++)
        {
            dialogueText.text += textToDisplay[i];
            yield return new WaitForSeconds(timeBetweenCharacters);
            if(skipSentence) i = textToDisplay.Length;
        }

        dialogueText.text = textToDisplay;
        skipSentence = false;
        typingSentence = false;

        if(pauseAfter) yield return new WaitForSeconds(2f);
        
        canMoveOn = true;

        StopCoroutine(TypeSentence(sentence));
    }

    //Blinks an icon to show the player that they can move to the next sentence
    IEnumerator BlinkIcon()
    {
        waitingForPlayerToContinue = true;
        while(waitingForPlayerToContinue) 
        {
            blinkIcon.enabled = true;
            yield return new WaitForSeconds(.5f);
            blinkIcon.enabled = false;
            yield return new WaitForSeconds(.3f);
        }
        waitingForPlayerToContinue = false;

        StopCoroutine(BlinkIcon());
    }

    IEnumerator FadeInBackgroundImage(Image image)
    {
        image.color = new Color(1,1,1,0);
        backgroundImageDisplay.enabled = true;

        while(image.color.a < 1)
        {
            image.color = new Color(1,1,1, image.color.a +.125f);
            yield return new WaitForSeconds(.045f);
        }

        StopCoroutine(FadeInBackgroundImage(image));
    }

    IEnumerator FadeOutBackgroundImage(Image image)
    {
        image.color = new Color(1,1,1,1);

        while(image.color.a > 0)
        {
            image.color = new Color(1,1,1, image.color.a -.125f);
            yield return new WaitForSeconds(.045f);
        }

        backgroundImageDisplay.enabled = false;
        StopCoroutine(FadeOutBackgroundImage(image));
    }

    private void DeactivateChoicePanels()
    {
        for(int i = 0; i < choicePanelsActive.Length; i++)
        {
            choicePanelsInactive[i].SetActive(false);
            choicePanelsActive[i].SetActive(false);
            choiceTextsInactive[i].text = "";
            choiceTextsActive[i].text = "";
        }
    }
}