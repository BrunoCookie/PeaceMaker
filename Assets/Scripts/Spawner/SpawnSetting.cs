using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SpawnSetting")]
public class SpawnSetting : ScriptableObject
{
    [Header("Angry")]
    public float shootStart;
    public float shootSpawnRate;

    [Header("Sad")]
    public float speedStart;
    public float speedSpawnRate;

    [Header("Nauseous")]
    public float slimeStart;
    public float slimeSpawnRate;
}
