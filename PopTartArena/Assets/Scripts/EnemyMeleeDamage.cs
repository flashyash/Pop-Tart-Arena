using UnityEngine;

public class EnemyMeleeDamage : MonoBehaviour
{
    public int maxHealth = 400;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Destroy the enemy or play death animation, etc.
        Destroy(gameObject);
    }
}
