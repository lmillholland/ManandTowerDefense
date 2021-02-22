using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonTower : Tower
{
    [Header("Cannon Tower Settings")]
    public GameObject cannonBallPrefab;
    public float cannonBallSpeed = 20f;

    GameObject cannonBall;
    
    public override void Fire()
    {
        // spawn cannonball at tower position, override with enemy y-coordinate
        var spawnPosition = new Vector3(transform.position.x, Target.transform.position.x, transform.position.z);
        cannonBall = Instantiate(cannonBallPrefab, transform.position, Quaternion.identity);
        base.Fire();
    }

    public override void HandleUpdate()
    {
        /*
        Move cannonball toward target.
        */
        if (cannonBall != null)
        {
            if (Target == null)
            {
                // in cases such as if the enemy's died
                Destroy(cannonBall);
            }
            else
            {
                // move cannonball
                var startPos = cannonBall.transform.position;
                var endPos = Target.transform.position;
                var step = cannonBallSpeed * Time.deltaTime;
                cannonBall.transform.position = Vector3.MoveTowards(startPos, endPos, step);

                // check if we've reached the enemy
                if (Vector3.Distance(cannonBall.transform.position, Target.transform.position) < 0.001f)
                {
                    Target.GetComponent<EnemyController>().TakeDamage(damage);
                    Destroy(cannonBall);
                }
            }
        }
        base.HandleUpdate();
    }
}


