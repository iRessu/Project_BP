using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class Menu_Script : MonoBehaviour
{
    public float time;
    private bool isCountingDown = false;

    public PlayableDirector timeline;
    public string sceneToLoad;


    private void Awake()
    {
        timeline.Stop();
    }

    private void Start()
    {
        FindObjectOfType<AudioManager>().PlaySound("MenuMusic");
    }
    private void Update()
    {
        if(isCountingDown)
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                isCountingDown = false;
                SceneManager.LoadScene(sceneToLoad);
            }
        }
   
    }
    public void StartGame()
    {
        StartTimeLine();
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Ja nu stängs ju spelet ner, det ser ju jag");
    }

    private void StartTimeLine()
    {
        FindObjectOfType<AudioManager>().StopMenuMusic();
        timeline.Play();
        isCountingDown = true;
    }
}
