using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PaladinLife : MonoBehaviour
{
    public static float maxHealth = 60 * AdaptativeDifficulty.actual_diff/10;
    private float enragedStartHealth = maxHealth/4;
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

        if (currentHealth <= enragedStartHealth && AdaptativeDifficulty.actual_diff >= 70)
            GetComponent<Animator>().SetBool("isEnraged", true);

        if (currentHealth <= 0)
            Die();
        healthBar.SetHealth(currentHealth);
    }
    public void Die()
    {
        Destroy(gameObject);
        AdaptativeDifficulty.boss_killed = true;
        AdaptativeDifficulty.CallMeDiffDaddy();
        RestartGame();
    }

    public void DamagePlayer()
    {
        PlayerLife.instance.LoseHealth();
    }

    public void EnragedDamagePlayer()
    {
        PlayerLife.instance.LoseEnragedHealth();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
