using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{

    [SerializeField] List<CameraGoal> goals;
    [SerializeField] List<CheckListObject> checkListObjects;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckOffGoal(CameraGoal goal)
    {
        for(int i = 0; i < goals.Count; i++)
        {
            if(goal = goals[i])
            {
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

    }
}
