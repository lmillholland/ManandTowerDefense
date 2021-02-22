using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveController : MonoBehaviour
{
    float speed;
    EnemyController controller;
    GameObject currWayPoint;
    int currWayPointIdx = 0;
    float minDistToWayPoint = 1f; // necessary since enemies are different sizes

    void Start()
    {
        // Read speed from EnemyController
        controller = GetComponent<EnemyController>();
        speed = controller.speed;

        // tracks current waypoint
        currWayPoint = EnemyManager.instance.wayPoints[currWayPointIdx];
    }

    public void HandleUpdate()
    {
        /*
        Move towards next waypoint.
        Note: last waypoint is position behind castle, so should never be reached.
        */
        // pick next waypoint if we don't have a current target
        if (currWayPoint == null)
            currWayPoint = EnemyManager.instance.wayPoints[currWayPointIdx];

        // move toward waypoint
        var startPos = transform.position;
        var targetPos = currWayPoint.transform.position;
        var step = Time.deltaTime * speed * LevelManager.instance.FastForwardSpeed;
        transform.position = Vector3.MoveTowards(startPos, targetPos, step);

        // check if we've reached waypoint
        // TODO: ignore y coordinate
        if (Vector3.Distance(transform.position, currWayPoint.transform.position) < minDistToWayPoint)
        {
            currWayPoint = null;
            currWayPointIdx++;
        }
    }
}
