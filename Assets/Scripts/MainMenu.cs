using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public LevelLoader levelLoader;
    public RectTransform brainIcons;
    public AudioManager audiomanager;
    public Animator transition;
    public void iconToPlayButton()
    {
        brainIcons.localPosition = new Vector3(0, 261);
    }

    public void iconToOptionsButton()
    {
        brainIcons.localPosition = new Vector3(0, 125);
    }

    public void iconToScoreButton()
    {
        brainIcons.localPosition = new Vector3(0, -11);
    }

    public void iconToCreditsButton()
    {
        brainIcons.localPosition = new Vector3(0, -147);
    }

    public void iconToExitButton()
    {
        brainIcons.localPosition = new Vector3(0, -332);
    }

    public void StartGame()
    {
        StartCoroutine(PressPlay());
        
    }

    IEnumerator PressPlay()
    {
        audiomanager.Stop("MainMenu");
        yield return new WaitForSeconds(0.75f);
        StartCoroutine(levelLoader.LoadLevel(1));
    }

    public void ExitGame()
    {
        StartCoroutine(ExitTheGame());
    }

    IEnumerator ExitTheGame()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        Application.Quit();
    }
}
