using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float destroyTime;
    public GameObject DestroyParticle;
    public Gradient particleGradient;


    private void Start()
    {
        Invoke("DestroyGameObject", destroyTime);
    }

    private void DestroyGameObject()
    {
        if(DestroyParticle != null)
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
}
