using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Mate : MonoBehaviour
{
    public static int mateCount;

    public Transform spot;
    [HideInInspector] public bool onSpot = false;
    public Rigidbody2D rb;
    public float speed;

    private Vector2 moveDir;
    void Start()
    {
        //Check if another Mate is allowed
        mateCount++;
        if (mateCount > 8)
        {
            Destroy(gameObject);
            return;
        }

        Physics2D.IgnoreLayerCollision(gameObject.layer, gameObject.layer);

        //Search for MateSpot to be in place
        for(int i = 0; i<GameMode.instance.mateSpots.Count; i++)
        {
            if (GameMode.instance.mateSpots[i].childCount <= 0)
            {
                spot = GameMode.instance.mateSpots[i];
                GetComponent<HoldDistanceToTarget>().target = spot;
                transform.parent = spot;
                break;
            }
        }
    }

    private void FixedUpdate()
    {
        if (onSpot || spot == null) return;

        moveDir = spot.position - transform.position;

        if (moveDir.magnitude < 0.1f) {
            
            rb.isKinematic = true;
            onSpot = true;
            GetComponent<HoldDistanceToTarget>().enabled = false;
            GetComponent<Life>().invincible = false;
        }
        

        //moveDir = moveDir.normalized;
        //rb.MovePosition(rb.position + moveDir * Time.fixedDeltaTime * speed);
    }

    private void OnDestroy()
    {
        mateCount--;
    }

    public void ChangeTarget(Transform newTarget)
    {
        transform.parent = newTarget;
        spot = newTarget;
        GetComponent<HoldDistanceToTarget>().target = newTarget;
        GetComponent<HoldDistanceToTarget>().enabled = true;
        onSpot = false;
    }
}
