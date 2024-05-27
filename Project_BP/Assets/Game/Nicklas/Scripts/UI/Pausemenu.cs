using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pausemenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;


    private void Start()
    {
        pauseMenu.SetActive(false);
    }
    private void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Main_Menu");
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Debug.Log("Nu stängs spelet ner, dåra");
        Application.Quit();
    }

}
