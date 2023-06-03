using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float restartDelay = 3;
    public GameObject levelLoader;

    private LevelLoader lL;

    private void Start()
    {
        lL = levelLoader.GetComponent<LevelLoader>();
    }

    public void Win()
    {
        lL.LoadNextLevel();
    }

    public void Lose()
    {
        StartCoroutine(Restart());
    }

    protected virtual IEnumerator Restart()
    {
        yield return new WaitForSeconds(restartDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
