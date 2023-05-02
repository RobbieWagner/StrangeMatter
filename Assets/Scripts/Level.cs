using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{

    [SerializeField] List<CameraGoal> goals;
    [SerializeField] List<CheckListObject> checkListObjects;

    [SerializeField] GameObject completeLevelButton;

    // Start is called before the first frame update
    void Start()
    {
        completeLevelButton.SetActive(false);
    }

    public void CheckOffGoal(CameraGoal goal)
    {
        for(int i = 0; i < goals.Count; i++)
        {
            if(goal = goals[i])
            {
                if(goals[i].goalEffect != null) goals[i].goalEffect.ActivateGoalEffect();
                checkListObjects[i].CheckBox();
                checkListObjects.RemoveAt(i);
                goals.RemoveAt(i);
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
}
