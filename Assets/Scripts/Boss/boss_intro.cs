using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_intro : StateMachineBehaviour
{
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Transform player = GameMode.instance.player.transform;
        float dis = Vector2.Distance(player.position, animator.transform.position);
        if (dis < 5f) player.GetComponent<Rigidbody2D>().AddForce( (player.position - animator.transform.position).normalized * 2000f, ForceMode2D.Force);

        GameMode.instance.audiomanager.Stop("GameTheme");
        GameMode.instance.audiomanager.Play("BossMusic");
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
