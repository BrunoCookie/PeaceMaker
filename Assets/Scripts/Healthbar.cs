using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Life lifeScript;
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    private void Start()
    {
        if (lifeScript == null)
        {
            Debug.LogWarning(name + "has no LifeScript!");
            gameObject.SetActive(false);
        }
            
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public float GetMaxHealth()
    {
        return slider.maxValue;
    }

    private void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        SetHealth(lifeScript.health);
    }

}
