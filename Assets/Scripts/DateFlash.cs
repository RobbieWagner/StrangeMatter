using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DateFlash : MonoBehaviour
{

    [SerializeField] Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FlashDate());
    }


    IEnumerator FlashDate()
    {
        canvas.enabled = true;
        yield return new WaitForSeconds(3f);
        canvas.enabled = false;
    }
}
