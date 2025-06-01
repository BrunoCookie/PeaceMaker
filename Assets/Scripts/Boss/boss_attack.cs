using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_attack : StateMachineBehaviour
{
    public float attackTime;
    public GameObject ShootEnemy, SpeedEnemy, SlimeEnemy;

    private float counter = 0f;
    private float spawnCounter;
    private float spawnRate;    // = 1 - GameMode.instance.level * 0.25f;
    //private Transform[] spawnSpots = new Transform[3];

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        counter = 0f;
        spawnRate = 0.85f - GameMode.instance.level * 0.1f;
        spawnCounter = Time.time + spawnRate;
        animator.GetComponent<Life>().invincible = true;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        counter += Time.deltaTime;
        if (counter >= attackTime) animator.SetBool("isVulnerable", true);

        if (Time.time >= spawnCounter)
        {
            int spotIndex = Random.Range(0, 3);
            SpawnEnemy(animator.transform.GetChild(1).GetChild(spotIndex).position);
            spawnCounter = Time.time + spawnRate;
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    public void SpawnEnemy(Vector3 pos)
    {
        float rnd = Random.Range(0f, 1f);

        if(rnd >= 0.66f) //SpawnShoot
        {
            Transform enemy = Instantiate(ShootEnemy, pos, Quaternion.identity).transform;
            enemy.GetComponent<Enemy>().target = GameMode.instance.player.transform;
            enemy.GetComponent<Enemy>().ChangeState(Enemy.EnemyState.Chase);
        }
        else if(rnd >= 0.33f) //SpawnSpeed
        {
            Transform enemy = Instantiate(SpeedEnemy, pos, Quaternion.identity).transform;
            enemy.GetComponent<Enemy>().target = GameMode.instance.player.transform;
            enemy.GetComponent<Enemy>().ChangeState(Enemy.EnemyState.Chase);
        }
        else //SpawnSlime
        {
            Transform enemy = Instantiate(SlimeEnemy, pos, Quaternion.identity).transform;
            enemy.GetComponent<Enemy>().ChangeState(Enemy.EnemyState.Chase);
        }
    }
}
