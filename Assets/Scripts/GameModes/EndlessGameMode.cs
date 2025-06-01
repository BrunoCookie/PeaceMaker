using UnityEngine;

public class EndlessGameMode : GameMode
{
    bool notFinished = true;

    protected override void  Update()
    {
        base.Update();

        #region winloseCondition
        playerHealth = player.GetComponent<Life>().health;
        brainHealth = brain.GetComponent<Life>().health;

        if ((playerHealth <= 0 || brainHealth <= 0) && notFinished)
        {
            //Debug.Log("YOU LOST!");
            EndGame();
        }
        #endregion
    }

    public void EndGame()
    {
        //player.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        //brain.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        Score.instance.AddTimePoints((int)(Time.timeSinceLevelLoad) * 3);
        pauseMenu.enabled = false;
        GameUI.SetActive(false);
        DeathScreenUI.SetActive(true);
        Time.timeScale = 0f;
        notFinished = false;
    }
}
