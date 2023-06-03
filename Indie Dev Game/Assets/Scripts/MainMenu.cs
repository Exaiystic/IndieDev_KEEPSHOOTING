using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject levelLoader;
    public GameObject[] menuButtons;

    private LevelLoader lL;

    private void Start()
    {
        lL = levelLoader.GetComponent<LevelLoader>();
    }

    public void PlayGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        lL.LoadNextLevel();
        for (int i = 0; i < menuButtons.Length; i++)
        {
            menuButtons[i].SetActive(false);
        }
    }

    public void QuitGame()
    {
        Debug.Log("Game Quitted");
        Application.Quit();
    }
}
