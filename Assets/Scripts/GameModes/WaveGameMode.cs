using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveGameMode : GameMode
{

    protected override void Update()
    {
        base.Update();
        #region winloseCondition
        playerHealth = player.GetComponent<Life>().health;
        brainHealth = brain.GetComponent<Life>().health;

        if (enemiesList.Count <= 0 && wavesList.Count <= 0)
        {
            Debug.Log("YOU WIN!");
        }
        else if (playerHealth <= 0 || brainHealth <= 0)
        {
            Debug.Log("YOU LOST!");
        }
        #endregion
    }
}
