using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeasurementGoal : MonoBehaviour
{
    MeasureButton telescopeRuler;
    [SerializeField] public GoalEffect goalEffect;
    [SerializeField] public string goalAchievementText;

    private void Start() 
    {
        telescopeRuler = GameObject.Find("MeasureButton").GetComponent<MeasureButton>();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Camera"))
        {
            telescopeRuler.EnableRuler(this);
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Camera"))
        {
            telescopeRuler.DisableRuler();
        }
    }
}
