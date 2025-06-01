using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeSpawner : WaveSpawner
{
    public Transform aim; //Same as target

    protected override void Spawn()
    {
        GameObject enemy = Instantiate(prefab, transform.position, transform.rotation);
        enemy.GetComponent<HoldDistanceToTarget>().target = aim;
        enemy.GetComponent<ShootAtTarget>().target = aim;
        enemy.transform.GetChild(1).GetComponent<RotateTowardsTarget>().target = aim;
    }
}
