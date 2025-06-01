using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public int damage = 0;
    private static int enemiesKilled = 0;

    public GameObject DestroyParticle;
    public Gradient particleGradient;
    //private bool doEffect = true;

    public GameObject bossParticle;
    public Gradient bossGradient;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            if (DestroyParticle != null)
            {
                ParticleSystem ps = Instantiate(DestroyParticle, transform.position, Quaternion.identity).GetComponent<ParticleSystem>();
                if (particleGradient != null)
                {
                    var col = ps.colorOverLifetime;
                    col.color = particleGradient;
                }
            }
            Destroy(gameObject);
        }

        if (collision.tag == "Enemy" || collision.tag == "Boss")
        {
            collision.GetComponent<Life>().TakeDamage(damage);
            if(collision.GetComponent<Life>().health <= 0)
            {
                Score.instance.AddEnemyPoints(10);
                enemiesKilled++;
                if (enemiesKilled % 10 == 0)
                {
                    Instantiate(GameMode.instance.mate, transform.position, Quaternion.identity);
                    GameMode.instance.audiomanager.Play("MateCollect");
                    Score.instance.AddEnemyPoints(20);
                }
            }
            if(collision.tag == "Boss")
            {
                ParticleSystem ps = Instantiate(bossParticle, transform.position, Quaternion.identity).GetComponent<ParticleSystem>();
                if (particleGradient != null)
                {
                    var col = ps.colorOverLifetime;
                    col.color = bossGradient;
                }
            }
            //doEffect = false;
            Destroy(gameObject);
        }
    }
}
