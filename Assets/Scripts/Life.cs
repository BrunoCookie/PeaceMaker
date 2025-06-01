using UnityEngine;
using UnityEngine.Events;

public class Life : MonoBehaviour
{

    public int maxHealth;
    public int health;
    public bool invincible;
    public string hurtSound;
    public UnityEvent onDamageTaken;

    public void TakeDamage(int dmg)
    {
        if (invincible) return;
        health = Mathf.Max(0, health - dmg);
        if (hurtSound != null && hurtSound != "") GameMode.instance.audiomanager.Play(hurtSound);
        onDamageTaken.Invoke();
    }

    public void Heal(int heal)
    {
        health = Mathf.Min(health + heal, maxHealth);
    }
}
