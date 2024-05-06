using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Script : MonoBehaviour
{

    public Animator animationController;
    public string sceneToLoad;

    
    public void StartGame()
    {
        StartCoroutine(StartGameAnimation());
    }


    private IEnumerator StartGameAnimation()
    {
        animationController.SetTrigger("StartAnimation");

        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(sceneToLoad);
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Ja nu stängs ju spelet ner, det ser ju jag");
    }
}
