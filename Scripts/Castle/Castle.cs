using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{
    HUDController hud;
    public float maxHealth = 100f;

    void Awake()
    {
        hud = GetComponentInChildren<HUDController>();
    }

    void Start()
    {
        hud.Initialize(null, maxHealth);
    }

    void OnCollisionEnter(Collision collision)
    {
        /*
        Take damage from colliding enemies (and destroy this enemies).
        */
        if (collision.gameObject.tag == "Enemy")
        {
            var enemy = collision.gameObject.GetComponent<EnemyController>();
            if (enemy.canHurtCastle)
            {
                enemy.canHurtCastle = false; // handles race conditions
                var damage = enemy.damage; // store this since we're about to delete enemy
                enemy.Die(true);
                TakeDamage(damage);
            }
        }
    }

    void TakeDamage(float damage)
    {
        hud.TakeDamage(damage);
    }
}
