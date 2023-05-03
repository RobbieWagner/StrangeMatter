using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Level : MonoBehaviour
{

    [SerializeField] List<CameraGoal> goals;
    [SerializeField] List<MeasurementGoal> mGoals;
    [SerializeField] List<CheckListObject> checkListObjects;

    [SerializeField] GameObject completeLevelButton;

    [SerializeField] TextMeshProUGUI goalAchievementText;
    [SerializeField] TextMeshProUGUI measureText;

    [SerializeField] List<GoalEffect> afterChecklistEffects;

    [SerializeField] GoalEffect lastGoalEffect;

    // Start is called before the first frame update
    void Start()
    {
        completeLevelButton.SetActive(false);
    }

    public void CheckOffGoal(CameraGoal goal)
    {
        bool removedElement = false;
        for(int i = 0; i < goals.Count; i++)
        {
            CameraGoal listedGoal = goals[i];
            CheckListObject checkListObject = checkListObjects[i];
            if(goal == goals[i] && !removedElement)
            {
                if(listedGoal.goalEffect != null) listedGoal.goalEffect.ActivateGoalEffect();

                removedElement = true;
                checkListObjects.RemoveAt(i);
                goals.RemoveAt(i);

                StartCoroutine(FlashGoalCompletionText(listedGoal));

                if(!goals.Contains(goal)) 
                {
                    if(checkListObject.isMultiStageTask) checkListObject.IncrementGoal();
                    else checkListObject.CheckBox();
                }
                else 
                {
                    checkListObject.IncrementGoal();
                }
            }
        }

        if(goals.Count == 0 && mGoals.Count == 0)
        {
            CompleteLevel();
        }
        else if(goals.Count + mGoals.Count == afterChecklistEffects.Count)
        {
            afterChecklistEffects[0].ActivateGoalEffect();
            afterChecklistEffects.RemoveAt(0);
        }
    }

    public void CheckOffGoal(MeasurementGoal goal)
    {
        bool removedElement = false;
        for(int i = 0; i < mGoals.Count; i++)
        {
            MeasurementGoal listedGoal = mGoals[i];
            CheckListObject checkListObject = checkListObjects[i + goals.Count];
            if(goal == mGoals[i] && !removedElement)
            {
                if(listedGoal.goalEffect != null) listedGoal.goalEffect.ActivateGoalEffect();

                removedElement = true;
                checkListObjects.RemoveAt(i + goals.Count);
                mGoals.RemoveAt(i);

                StartCoroutine(FlashGoalCompletionText(listedGoal));

                if(!mGoals.Contains(goal)) 
                {
                    if(checkListObject.isMultiStageTask) checkListObject.IncrementGoal();
                    else checkListObject.CheckBox();
                }
                else 
                {
                    checkListObject.IncrementGoal();
                }
            }
        }

        if(goals.Count == 0 &&  mGoals.Count == 0)
        {
            CompleteLevel();
        }
        else if(goals.Count + mGoals.Count == afterChecklistEffects.Count)
        {
            afterChecklistEffects[0].ActivateGoalEffect();
            afterChecklistEffects.RemoveAt(0);
        }
    }

    private void CompleteLevel()
    {
        completeLevelButton.SetActive(true);

        if(lastGoalEffect != null) lastGoalEffect.ActivateGoalEffect();
    }

    private IEnumerator FlashGoalCompletionText(CameraGoal goal)
    {
        goalAchievementText.text = goal.goalAchievementText;
        goalAchievementText.enabled = true;

        yield return new WaitForSeconds(3f);

        goalAchievementText.enabled = false;
    }

    private IEnumerator FlashGoalCompletionText(MeasurementGoal goal)
    {
        measureText.text = goal.goalAchievementText;
        measureText.enabled = true;

        yield return new WaitForSeconds(3f);

        measureText.enabled = false;
    }
}
