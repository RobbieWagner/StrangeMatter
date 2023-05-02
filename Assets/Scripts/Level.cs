using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Level : MonoBehaviour
{

    [SerializeField] List<CameraGoal> goals;
    [SerializeField] List<CheckListObject> checkListObjects;

    [SerializeField] GameObject completeLevelButton;

    [SerializeField] TextMeshProUGUI goalAchievementText;

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
                    checkListObject.CheckBox();
                    if(checkListObject.isMultiStageTask) checkListObject.IncrementGoal();
                }
                else 
                {
                    checkListObject.IncrementGoal();
                }
            }
        }

        if(goals.Count == 0)
        {
            CompleteLevel();
        }
    }

    private void CompleteLevel()
    {
        completeLevelButton.SetActive(true);
    }

    private IEnumerator FlashGoalCompletionText(CameraGoal goal)
    {
        goalAchievementText.text = goal.goalAchievementText;
        goalAchievementText.enabled = true;

        yield return new WaitForSeconds(3f);

        goalAchievementText.enabled = false;
    }
}
