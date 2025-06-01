using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MoodSystem : MonoBehaviour
{
    public static MoodSystem instance;
    
    [Header("List of possible MoodSettings")]
    public MoodSetting[] settings = new MoodSetting[5];
    private int settingsIndex = 0;
    private MoodSetting currentSetting;
    private float currentTimePassed = 0f;

    [Header("Attributes to refresh the UI")]
    public Slider slider;
    public Image bar;
    public Image dude;
    public List<Sprite> dudeSprites = new List<Sprite>();

    [Header("Gameplay related")]
    public MoodSetting.moods currentMood;
    public UnityEvent onMoodChanged;

    private void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Debug.LogWarning("More than 1 MoodSystem detected!");
            gameObject.SetActive(false);
            return;
        }

    }

    private void Start()
    {
        changeSetting(settings[0]);
    }

    private void Update()
    {
        if (currentMood != currentSetting.getMood(currentTimePassed - Time.deltaTime)) onMoodChanged.Invoke();

        currentTimePassed += Time.deltaTime;

        if(currentTimePassed > currentSetting.length)
        {
            int rnd = Random.Range(0, settings.Length);
            while(rnd == settingsIndex) rnd = Random.Range(0, settings.Length);
            settingsIndex = rnd;
            changeSetting(settings[settingsIndex]);
        }

        //slider.value = currentTimePassed;
        currentMood = currentSetting.getMood(currentTimePassed);
    }

    public void changeSetting(MoodSetting newSetting)
    {
        currentTimePassed = 0f;
        currentSetting = newSetting;
        //slider.maxValue = currentSetting.length;
        //bar.sprite = currentSetting.barImage;
        currentMood = currentSetting.getMood(0f);
        onMoodChanged.Invoke();
    }

    public void RefreshDude()
    {
        switch (currentMood)
        {
            default:
            case MoodSetting.moods.happy:
                dude.sprite = dudeSprites[0];
                break;
            case MoodSetting.moods.angry:
                dude.sprite = dudeSprites[1];
                break;
            case MoodSetting.moods.sad:
                dude.sprite = dudeSprites[2];
                break;
            case MoodSetting.moods.nausea:
                dude.sprite = dudeSprites[3];
                break;
            case MoodSetting.moods.mentalbreakdown:
                dude.sprite = dudeSprites[4];
                break;
        }
        //Debug.Log("Dude has been refreshed!");
    }
}
