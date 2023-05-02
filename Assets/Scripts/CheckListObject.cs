using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CheckListObject : MonoBehaviour
{
    [SerializeField] private Image checkBox;
    [SerializeField] TextMeshProUGUI taskText;
    public bool isMultiStageTask;
    [SerializeField] private bool listSteps = true;
    [SerializeField] int steps;
    int stepsComplete;
    string originalTask;


    // Start is called before the first frame update
    void Start()
    {
        checkBox.enabled = false;
        if(isMultiStageTask)
        {
            stepsComplete = 0;
            originalTask = taskText.text;
            if(listSteps) taskText.text += " (" + stepsComplete + "/" + steps + ")";
        }
    }

    public void CheckBox()
    {
        checkBox.enabled = true;
    }

    public void IncrementGoal()
    {
        stepsComplete++;
        if(listSteps) taskText.text = originalTask + " (" + stepsComplete + "/" + steps + ")";
    }
}
