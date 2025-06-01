using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    [Header("Enemies")]
    public GameObject shootEnemy;
    public GameObject speedEnemy;
    public GameObject slimeEnemy;

    [Header("Rails")]
    public List<Rail> shootRails = new List<Rail>();
    public List<Rail> speedRails = new List<Rail>();
    public List<Rail> slimeRails = new List<Rail>();

    [Header("Spawn Settings")]
    public List<SpawnSetting> happySettings = new List<SpawnSetting>();
    public List<SpawnSetting> angrySettings = new List<SpawnSetting>();
    public List<SpawnSetting> sadSettings = new List<SpawnSetting>();
    public List<SpawnSetting> nauseaSettings = new List<SpawnSetting>();


    private SpawnSetting currentSpawnSetting;
    private static int enemiesSpawned = 0;
    private static int shootCount = 0;
    private static int speedCount = 0;
    private static int slimeCount = 0;

    void Start()
    {
        MoodSystem.instance.onMoodChanged.AddListener(RefreshSpawnSetting);
        RefreshSpawnSetting();
    }

    public void SpawnShootEnemy()
    {
        enemiesSpawned++;
        Rail railToSpawn = shootRails[shootCount % 3];

        Transform aim = CheckForAim();
        GameObject enemy = Instantiate(shootEnemy, railToSpawn.nodes[0].transform.position, transform.rotation);
        enemy.GetComponent<Enemy>().target = aim;
        enemy.GetComponent<FollowRail>().rail = railToSpawn;

        shootCount++;
    }

    public void SpawnSpeedEnemy()
    {
        enemiesSpawned++;
        Rail railToSpawn = speedRails[speedCount % 3];
        if (railToSpawn == null) return;

        Transform aim = CheckForAim();
        GameObject enemy = Instantiate(speedEnemy, railToSpawn.nodes[0].transform.position, transform.rotation);
        enemy.GetComponent<Enemy>().target = aim;
        enemy.GetComponent<FollowRail>().rail = railToSpawn;

        speedCount++;
    }

    public void SpawnSlimeEnemy()
    {
        enemiesSpawned++;
        Rail railToSpawn = slimeRails[slimeCount % 3];
        if (railToSpawn == null) return;

        Transform aim = CheckForAim();
        GameObject enemy = Instantiate(slimeEnemy, railToSpawn.nodes[0].transform.position, transform.rotation);
        enemy.GetComponent<Enemy>().target = aim;
        enemy.GetComponent<FollowRail>().rail = railToSpawn;

        slimeCount++;
    }

    private Transform CheckForAim()
    {
        Transform aim;
        if (GameMode.instance.fullShield || enemiesSpawned % 4 == 0)
        {
            aim = GameMode.instance.brain.transform;
        }
        else aim = GameMode.instance.player.transform;

        return aim;
    }

    public void RefreshSpawnSetting()
    {
        CancelInvoke();
        MoodSetting.moods currentmood = MoodSystem.instance.currentMood;
        switch (currentmood)
        {
            default:
            case MoodSetting.moods.happy:
                currentSpawnSetting = happySettings[GameMode.instance.level];
                break;
            case MoodSetting.moods.angry:
                currentSpawnSetting = angrySettings[GameMode.instance.level];
                break;
            case MoodSetting.moods.sad:
                currentSpawnSetting = sadSettings[GameMode.instance.level];
                break;
            case MoodSetting.moods.nausea:
                currentSpawnSetting = nauseaSettings[GameMode.instance.level];
                break;
        }
            

        InvokeRepeating("SpawnShootEnemy", currentSpawnSetting.shootStart, currentSpawnSetting.shootSpawnRate);
        InvokeRepeating("SpawnSpeedEnemy", currentSpawnSetting.speedStart, currentSpawnSetting.speedSpawnRate);
        InvokeRepeating("SpawnSlimeEnemy", currentSpawnSetting.slimeStart, currentSpawnSetting.slimeSpawnRate);

        //Debug.Log("Spawn Setting Refreshed!");
    }

    public void StopSpawning()
    {
        CancelInvoke();
    }
}
