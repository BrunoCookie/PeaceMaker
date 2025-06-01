using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_vulnerable : StateMachineBehaviour
{
    public Transform shootEnemy;
    public float vulnerabilityTime;

    private float counter = 0f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        counter = 0f;
        animator.GetComponent<Life>().invincible = false;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        counter += Time.deltaTime;
        if (counter >= vulnerabilityTime) animator.SetBool("isVulnerable", false);
        if (animator.GetComponent<Life>().health <= 0f) animator.SetBool("isDead", true);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
