using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMode : MonoBehaviour
{
    public static GameMode instance;
    public AudioManager audiomanager;
    public GameObject player;
    public GameObject brain;
    public GameObject mate;
    public MBScript mbscript;
    public List<Rail> spawnRails;
    public Transform mateSpotsParent;
    public Slider brainHealthbar;
    public Transform shootSpots;
    public GameObject GameUI;
    public GameObject DeathScreenUI;
    public PauseMenu pauseMenu;
    public int level = 0;

    [HideInInspector] public List<WaveSpawner> wavesList = new List<WaveSpawner>();
    [HideInInspector] public List<Enemy> enemiesList = new List<Enemy>();
    [HideInInspector] public int playerHealth;
    [HideInInspector] public int brainHealth;
    [HideInInspector] public List<Transform> mateSpots = new List<Transform>();
     public bool fullShield = false;

    private void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Debug.LogWarning("More than 1 GameMode detected!");
            gameObject.SetActive(false);
            return;
        }

        playerHealth = player.GetComponent<Life>().health;
        brainHealth = brain.GetComponent<Life>().health;
        for (int i = 0; i<mateSpotsParent.childCount; i++)
        {
            mateSpots.Add(mateSpotsParent.GetChild(i));
        }

        Physics2D.IgnoreLayerCollision(9,9); //Enemies dont collide
    }

    protected virtual void Update()
    {
        //Debug.Log("YYY");
        if (Mate.mateCount < 8) fullShield = false;
        else fullShield = true;

        brainHealthbar.value = brain.GetComponent<Life>().health;
    }

    public void IncreaseLevel()
    {
        level += 1;
        if (level > 2) level = 2;
        MoodSystem.instance.onMoodChanged.Invoke();
    }
}
