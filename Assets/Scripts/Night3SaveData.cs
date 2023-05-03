using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Night3SaveData : MonoBehaviour
{

    public int ending;

    [HideInInspector] public bool removedDisk = false;
    [HideInInspector] public bool recordedMonster = false;

    // Start is called before the first frame update
    void Start()
    {
        int ending = 1;
    }

    public void SaveEnding()
    {
        if(removedDisk && recordedMonster) ending = 3;
        else if(removedDisk) ending = 2;
        else ending = 1;

        PlayerPrefs.SetInt("ending", ending);

        Debug.Log("ending saved");
    }
}
