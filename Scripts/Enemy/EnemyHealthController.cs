using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : HealthController
{
    EnemyController enemyController;

    void Start()
    {
        enemyController = GetComponentInParent<EnemyController>();
    }

    public override void Die()
    {
        enemyController.Die();
    }
}
