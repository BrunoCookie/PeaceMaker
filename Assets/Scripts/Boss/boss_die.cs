using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_die : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameMode.instance.audiomanager.Stop("BossMusic");
        GameMode.instance.audiomanager.Play("GameTheme");
        GameMode.instance.IncreaseLevel();
        Score.instance.AddBossPoints(500);
        if (!MBScript.playerHit) Score.instance.AddBossPoints(500);
        Destroy(animator.gameObject, stateInfo.length);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

}
