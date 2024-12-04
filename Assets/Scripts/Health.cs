using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    private bool isDead;

    void Start()
    {
        currentHealth = maxHealth;
        isDead = false;
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    private void Die()
    {
        if (isDead) return;

        isDead = true;
        FindObjectOfType<UIManager>().GameOver();
        // Handle other death-related logic, e.g., animations, disabling player control, etc.
    }

    public bool IsDead()
    {
        return isDead;
    }
}