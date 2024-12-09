using UnityEngine;
using UnityEngine.UI;

public class RangeLife : MonoBehaviour
{
    public float maxHealth = 20 * AdaptativeDifficulty.actual_diff/10;
    public float currentHealth;
    public HealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
            Die();
        healthBar.SetHealth(currentHealth);
    }
    public void Die()
    {
        Destroy(gameObject);
        AdaptativeDifficulty.monsters_killed += 1;
    }
}
