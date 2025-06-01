using TMPro;
using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    public LevelLoader loader;
    public TMP_InputField input;

    public void Safe()
    {
        //Debug.Log(input.text);
        Score.instance.SaveScore(input.text);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        StartCoroutine(loader.LoadLevel(1));
    }

    public void Menu()
    {
        Time.timeScale = 1f;
        StartCoroutine(loader.LoadLevel(0));
    }

}
