using UnityEngine;
using UnityEngine.UI;

public class AlienLife : MonoBehaviour
{
    private float maxHealth = 20 * AdaptativeDifficulty.actual_diff/10;
    private float currentHealth;
    public HealthBar healthBar;
    
    

    private void Start()
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
    private void Die()
    {
        Destroy(gameObject);
        AdaptativeDifficulty.monsters_killed += 1;
    }
}
