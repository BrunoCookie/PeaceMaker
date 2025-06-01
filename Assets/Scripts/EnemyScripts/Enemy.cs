using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyState { Spawn, Chase, Die }
    protected EnemyState currentState = EnemyState.Spawn;
    public Transform target;
    public GameObject deathEffect;
    public Gradient particleGradient;
    [Space(10)]

    public Life lifeScript;
    public GameObject mateToSpawn;
    public static int enemiesKilled = 0;

    protected virtual void Start()
    {
        if (target == null) target = GameMode.instance.player.transform;
        GameMode.instance.enemiesList.Add(this);
    }

    private void Update()
    {
        switch (currentState)
        {
            case EnemyState.Spawn:
                Spawn();
                break;

            case EnemyState.Chase:
                Chase();
                break;

            case EnemyState.Die:
                Die();
                break;
        }
    }

    protected virtual void Spawn()
    {
        //Follow the Rail

        //Transition
    }

    protected virtual void Chase()
    {
        //Search for Target and Attack

        //Transition
    }

    protected virtual void Die()
    {
        GameMode.instance.enemiesList.Remove(this);
        ParticleSystem ps = Instantiate(deathEffect, transform.position, Quaternion.identity).GetComponent<ParticleSystem>();
        if (particleGradient != null)
        {
            var col = ps.colorOverLifetime;
            col.color = particleGradient;
        }
        GameMode.instance.audiomanager.Play("EnemyHurt");
        Destroy(gameObject);
    }

    public virtual void ChangeState(EnemyState newState)
    {

    }

}
