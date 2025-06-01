using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnemyAI : Enemy
{
    #region Components
    public Rigidbody2D rb;

    public FollowRail followRail;
    public HoldDistanceToTarget holdDistance;
    public ShootAtTarget shootAtTarget;
    public RotateTowardsTarget rotateToTarget;

    public int collisionDamage;
    #endregion


    protected override void Start()
    {
        base.Start();

        //Initalisiere alle Components + deren Variablen und Verweise (CHECK)
        if(followRail.rail != null) 
            transform.position = followRail.rail.nodes[0].position;
        //followRail.rail = GameMode.instance.spawnRails[0];
        transform.GetComponent<Collider2D>().isTrigger = true;

        //Search for the right shootspot if target == brain, if no shootspot --> target == player
        holdDistance.target = this.target;
        if (target == GameMode.instance.brain.transform)
        {
            Transform spotTarget = GetShootSpot();
            if (spotTarget == null) target = GameMode.instance.player.transform;
            else
            {
                holdDistance.targetDistance = 0.1f;
                holdDistance.target = spotTarget;
                transform.parent = spotTarget;
            }
        }
        shootAtTarget.target = this.target;
        rotateToTarget.target = this.target;

        //Mach den Spawn State ready --> Disable die anderen Komponenten (CHECK)
    }

    protected override void Spawn()
    {
        //Follow the Rail (CHECK)

        //Transition (CHECK)
        if (followRail.isCompleted)
        {
            ChangeState(EnemyState.Chase);
        }
    }

    protected override void Chase()
    {
        //Search for Target and Attack (CHECK)

        //Transition
        if(lifeScript.health <= 0)
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
        else if(newState == EnemyState.Chase)
        {
            transform.GetComponent<Collider2D>().isTrigger = false;
            followRail.enabled = false;
            shootAtTarget.enabled = true;
            rotateToTarget.enabled = true;
            holdDistance.enabled = true;
        }
        else if (newState == EnemyState.Die)
        {
            holdDistance.enabled = false;
            shootAtTarget.enabled = false;
            rotateToTarget.enabled = false;
        }

        currentState = newState;
    }

    private Transform GetShootSpot()
    {
        Transform spotParent = GameMode.instance.shootSpots;
        Transform spot = spotParent.GetChild(0);
        int k = 0;
        while(k < spotParent.childCount && spot.childCount > 0)
        {
            spot = spotParent.GetChild(k);
            k++;
        }
        if (spot.childCount > 0) return null;
        else return spot;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player" || collision.collider.tag == "Brain")
        {
            collision.collider.GetComponent<Life>().TakeDamage(collisionDamage);
            ChangeState(EnemyState.Die);
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
        if (collision.tag == "Mate")
        {
            collision.GetComponent<Life>().TakeDamage(10);
            ChangeState(EnemyState.Die);
        }
    }
}
