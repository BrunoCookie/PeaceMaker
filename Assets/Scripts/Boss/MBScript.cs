using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MBScript : MonoBehaviour
{
    //UI
    public Slider slider;
    public Image dude;
    public Sprite dudeImage;
    public float length;


    //Boss
    public GameObject bossPrefab;
    [Space(20)]
    public UnityEvent onBossBeginn;
    public UnityEvent onBossEnd;
    public static bool playerHit = false;


    private Life life;
    private bool isBossActive = false;

    private void Awake()
    {
        slider.maxValue = length;
        slider.value = 0f;
    }

    private void Update()
    {
        if (!isBossActive)
        {
            slider.value += Time.deltaTime;
            if (slider.value >= length) BossStart();
        }
        else
        {
            slider.value = life.health;
            if (slider.value <= 0f) BossEnd();
        }

    }

    public void BossStart()
    {
        GameObject boss = Instantiate(bossPrefab, new Vector3(0, 11.86f, 0), Quaternion.identity);
        life = boss.GetComponent<Life>();
        slider.maxValue = life.maxHealth;
        slider.value = life.maxHealth;
        dude.sprite = dudeImage;
        onBossBeginn.Invoke();

        playerHit = false;
        isBossActive = true;
    }

    public void BossEnd()
    {
        length = Mathf.Min(length + 60f, 180f);
        onBossEnd.Invoke();
        ResetBar(length);

        isBossActive = false;
    }

    public void ResetBar(float _length)
    {
        length = _length;

        slider.maxValue = length;
        slider.value = 0f;
    }

    public void SetPlayerHit(bool hit)
    {
        playerHit = hit;
    }
}
