using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpeedEnemyAI : Enemy
{
    public int damage;
    #region Components
    public Rigidbody2D rb;

    public FollowRail followRail;
    public HoldDistanceToTarget holdDistance;
    #endregion

    protected override void Start()
    {
        base.Start();

        //Initalisiere alle Components + deren Variablen und Verweise (CHECK)
        if (followRail.rail != null)
            transform.position = followRail.rail.nodes[0].position;
        //followRail.rail = GameMode.instance.spawnRails[0];
        holdDistance.target = this.target;
    }

    protected override void Spawn()
    {

        //Transition
        if (followRail.isCompleted)
        {
            ChangeState(EnemyState.Chase);
        }
    }

    protected override void Chase()
    {
        if (lifeScript.health <= 0)
        {
            ChangeState(EnemyState.Die);
        }
    }

    protected override void Die()
    {
        //Play Animation?

        //Remove Object and spawn Mate
        base.Die();
    }

    public override void ChangeState(EnemyState newState)
    {
        if (newState == EnemyState.Spawn) Debug.LogWarning("Cannot change State to Spawn again");
        else if (newState == EnemyState.Chase)
        {
            transform.GetComponent<Collider2D>().isTrigger = false;
            followRail.enabled = false;
            holdDistance.enabled = true;
        }
        else if (newState == EnemyState.Die)
        {
            holdDistance.enabled = false;
        }

        currentState = newState;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player" || collision.collider.tag == "Brain" )
        {
            collision.collider.GetComponent<Life>().TakeDamage(damage);
            ChangeState(EnemyState.Die);
        }
        else if(collision.collider.tag == "Enemy")
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.collider);
        }

        if (collision.collider.tag == "Mate")
        {
            Life life = collision.collider.GetComponent<Life>();
            life.TakeDamage(10);
            if (life.health <= 0) Destroy(collision.gameObject);
            ChangeState(EnemyState.Die);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
    }

}
