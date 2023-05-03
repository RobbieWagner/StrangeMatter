using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionScene : MonoBehaviour
{

    [SerializeField] private string sceneName;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Camera"))
        {
            StartCoroutine(ChangeScene());
        }
    }

    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneName);

        StopCoroutine(ChangeScene());
    }
}
