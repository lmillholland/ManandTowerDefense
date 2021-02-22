using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spores : MonoBehaviour
{
    MushroomTower tower;

    void Start()
    {
        tower = GetComponentInParent<MushroomTower>();
    }

    void OnParticleCollision(GameObject other)
    {
        // Collide with & infect enemies.
        if (other.tag == "Enemy")
        {
            var enemy = other.GetComponent<EnemyController>();
            enemy.InflictStatus(tower.statusEffect);
        }
    }
}
