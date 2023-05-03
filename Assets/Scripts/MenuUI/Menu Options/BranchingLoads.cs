using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class BranchingLoads : LoadGameMenuItem
{

    [SerializeField] string[] scenes;
    [SerializeField] Night3SaveData saveData;

    public override void OnSelectMenuItem()
    {
        canvas.enabled = false;
        loadingCanvas.enabled = true;

        int ending = PlayerPrefs.GetInt("ending", 1);
        Debug.Log(ending);
        SceneManager.LoadScene(scenes[ending-1]);
    }
}
