using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{

    [SerializeField] RectTransform rect;
    [SerializeField] Vector2 movementVector;
    [SerializeField] bool moveAcrossY = true;
    [SerializeField] float maxValue = 100f;
    [SerializeField] float minValue = -100f;

    // Update is called once per frame
    void Update()
    {
        rect.anchoredPosition += movementVector * Time.deltaTime * 4f;

        if((moveAcrossY && (rect.anchoredPosition.y < minValue || rect.anchoredPosition.y > maxValue)) 
        ||( !moveAcrossY && (rect.anchoredPosition.x < minValue || rect.anchoredPosition.x > maxValue))) movementVector *= -1;
    }
}
