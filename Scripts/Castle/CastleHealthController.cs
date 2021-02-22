using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleHealthController : HealthController
{
    public override void Die()
    {
        // Indicate game is over AND we lost
        LevelManager.instance.GameOver(false);
    }
}
