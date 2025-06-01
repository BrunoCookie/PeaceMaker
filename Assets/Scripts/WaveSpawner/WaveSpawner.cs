using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public GameObject prefab;
    public float startTime;
    public float endTime;
    public float spawnCooldown;

    void Start()
    {
        GameMode.instance.wavesList.Add(this);
        InvokeRepeating("Spawn", startTime, spawnCooldown);
        Invoke("EndSpawn", endTime);
    }

    protected virtual void Spawn()
    {
        Instantiate(prefab, transform.position, transform.rotation);
    }

    protected void EndSpawn()
    {
        GameMode.instance.wavesList.Remove(this);
        Destroy(gameObject);
    }
}
