using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float movementSpeed;
    public GameObject slimePS;

    private Vector2 movement;
    private float ogSpeed;

    private void Awake()
    {
        ogSpeed = movementSpeed;
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * movementSpeed * Time.fixedDeltaTime);
    }

    public void SlowDown()
    {
        if (movementSpeed == 4) return;
        CancelInvoke();
        movementSpeed = 4;
        Invoke("ResetMovementSpeed", 2f);
        Instantiate(slimePS, transform.position + slimePS.transform.position, slimePS.transform.rotation, transform);
    }

    private void ResetMovementSpeed()
    {
        movementSpeed = ogSpeed;
    }
}
