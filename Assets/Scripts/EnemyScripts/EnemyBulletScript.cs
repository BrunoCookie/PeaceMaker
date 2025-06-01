using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    [HideInInspector]
    public int damage = 0;

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.layer == 8)
    //    {
    //        Destroy(gameObject);
    //    }

    //    if (collision.collider.tag == "Player" || collision.collider.tag == "Brain")
    //    {
    //        collision.collider.GetComponent<Life>().TakeDamage(damage);
    //        Destroy(gameObject);
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            Destroy(gameObject);
        }

        if (collision.tag == "Player" || collision.tag == "Brain")
        {
            collision.GetComponent<Life>().TakeDamage(damage);
            Destroy(gameObject);
        }

        if (collision.tag == "Mate")
        {
            Life life = collision.GetComponent<Life>();
            life.TakeDamage(10);
            if (life.health <= 0) Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
