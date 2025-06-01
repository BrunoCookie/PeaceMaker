using System.Collections;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject gameUI;
    public GameObject pauseMenuUI;
    public LevelLoader loader;
    public Animator transition;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        gameUI.SetActive(true);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    private void Pause()
    {
        gameUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        StartCoroutine(loader.LoadLevel(0));
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        StartCoroutine(ExitTheGame());
    }

    IEnumerator ExitTheGame()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        Application.Quit();
    }
}
