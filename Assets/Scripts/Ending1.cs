using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Ending1 : MonoBehaviour
{
    
    [SerializeField] AudioSource[] screams;
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3f);

        image.enabled = false;
        for(int i = 0; i < 1; i++)
        {
            foreach(AudioSource scream in screams)
            {
                scream.Play();
                yield return new WaitForSeconds(.2f);
            }
        }
        yield return new WaitForSeconds(1f);

        image.enabled = true;
        text.enabled = true;

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene("MainMenu");
    }
}
