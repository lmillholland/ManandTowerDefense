using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MamaController : EnemyController
{
    [Header("Mama Enemy Settings")]
    public int numBabies;
    public EnemyType babyEnemyType;

    public override void Die(bool crashedIntoCastle=false)
    {
        /*
        If Mama hasn't crashed into castle, birth a litter of baby enemies.
        */
        if (!crashedIntoCastle)
            EnemyManager.instance.SpawnEnemiesAtPos(babyEnemyType, transform.position, level, numBabies);
        base.Die(crashedIntoCastle);
    }
}
