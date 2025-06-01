using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldDistanceToTarget : MonoBehaviour
{
    public Transform target;
    public Rigidbody2D rb;
    public float speed;
    public float targetDistance;
    private float actualDistance;
    private Vector2 moveDir;
    private Vector2 closestPoint;

    private void Start()
    {
        if (target == null) return;

        if (target.GetComponent<Collider2D>() != null) closestPoint = target.GetComponent<Collider2D>().ClosestPoint(transform.position);
        else closestPoint = target.position;

        actualDistance = Vector2.Distance(closestPoint, transform.position);
    }

    private void Update()
    {
        if (target == null) return;

        if (target.GetComponent<Collider2D>() != null) closestPoint = target.GetComponent<Collider2D>().ClosestPoint(transform.position);
        else closestPoint = target.position;
        actualDistance = Vector3.Distance(closestPoint, transform.position);

        //moveDir = closestPoint - (Vector2)transform.position; //Move towards closest Point on Collider
        moveDir = target.position - transform.position;
        moveDir = moveDir.normalized;

    }

    private void FixedUpdate()
    {
        if (actualDistance > targetDistance)
        {
            rb.MovePosition(rb.position + moveDir * speed * Time.fixedDeltaTime);
        }
        else if (actualDistance < targetDistance - 0.2f)
        {
            rb.MovePosition(rb.position - moveDir * speed * Time.fixedDeltaTime * 2f);
        }
    }
}
