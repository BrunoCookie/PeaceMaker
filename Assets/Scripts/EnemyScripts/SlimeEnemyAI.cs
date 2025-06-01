using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeEnemyAI : Enemy
{
    public Rigidbody2D rb;
    public FollowRail followRail;
    public Vector2 startVelocity;
    public float speed;
    public Collider2D col;
    public int damage;
    public TrailRenderer tr;
    public float slimeTime;
    public GameObject slimeTrigger;

    private Vector2 lastVelocity;
    private GameObject triggerParent;

    protected override void Start()
    {
        base.Start();

        if (followRail.rail != null)
            transform.position = followRail.rail.nodes[0].position;
        //followRail.rail = GameMode.instance.spawnRails[0];
        triggerParent = new GameObject("TriggerParent");
        tr.time = slimeTime;
        slimeTrigger.GetComponent<DestroyAfterTime>().destroyTime = slimeTime;
    }

    private void FixedUpdate()
    {
        transform.rotation *= Quaternion.Euler(0, 0, -5f);
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
        lastVelocity = rb.velocity;
        

        //Transition
        if (lifeScript.health <= 0 || transform.position.y < -9f)
        {
            ChangeState(EnemyState.Die);
        }
    }

    protected override void Die()
    {
        base.Die();
    }

    public override void ChangeState(EnemyState newState)
    {
        if (newState == EnemyState.Spawn) Debug.LogWarning("Cannot change State to Spawn again");
        else if (newState == EnemyState.Chase)
        {
            //transform.GetComponent<Collider2D>().isTrigger = false;
            followRail.enabled = false;
            tr.enabled = true;
            if (transform.position.x > 0) startVelocity.x *= -1;
            rb.velocity = startVelocity.normalized * speed;
            InvokeRepeating("SpawnTrigger", 0f, 0.05f);
        }
        else if (newState == EnemyState.Die)
        {
            CancelInvoke();
            Transform gfx = transform.GetChild(0);
            triggerParent.transform.parent = gfx;
            gfx.GetComponent<SpriteRenderer>().enabled = false;
            DestroyAfterTime t = gfx.gameObject.AddComponent<DestroyAfterTime>();
            t.destroyTime = slimeTime;
            gfx.parent = null;
        }

        currentState = newState;
    }

    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Enemy")
        {
            Physics2D.IgnoreCollision(collision.collider, col);
        }

        if (currentState == EnemyState.Chase && collision.gameObject.layer == 8) //If is Chasing and touched layer
        {
            //Debug.Log("Wall collision!");
            rb.velocity = Vector2.Reflect(lastVelocity.normalized, collision.contacts[0].normal) * speed;
        }

        if(collision.collider.tag == "Player" || collision.collider.tag == "Brain")
        {
            collision.collider.GetComponent<Life>().TakeDamage(damage);
            ChangeState(EnemyState.Die);
        }

    }
    */

    private void SpawnTrigger()
    {
        Instantiate(slimeTrigger, transform.position, Quaternion.identity, triggerParent.transform);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (currentState == EnemyState.Chase && collision.gameObject.layer == 8) //If is Chasing and touched Wall
        {
            //Debug.Log("Wall collision!");
            rb.velocity = new Vector2(rb.velocity.x * -1, rb.velocity.y);
        }

        if (collision.tag == "Player" || collision.tag == "Brain")
        {
            collision.GetComponent<Life>().TakeDamage(damage);
            ChangeState(EnemyState.Die);
        }

        if (collision.tag == "Mate")
        {
            Life life = collision.GetComponent<Life>();
            life.TakeDamage(10);
            if (life.health <= 0) Destroy(collision.gameObject);
            ChangeState(EnemyState.Die);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)startVelocity.normalized * 2);
    }
}
