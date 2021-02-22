using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HealthController : MonoBehaviour
{
    [SerializeField] HealthBarController healthBarController;
    float maxHealth;
    float health;

    public void Initialize(float maxHealth)
    {
        this.maxHealth = maxHealth;
        health = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        bool isCritical = false; // TODO
        healthBarController.UpdateHealth(damage, health / maxHealth, isCritical);
        if (health <= 0)
            Die();
    }

    public abstract void Die();
}
