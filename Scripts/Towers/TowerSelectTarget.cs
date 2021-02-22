using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSelectTarget : MonoBehaviour
{
    public float rangeBuffer = 1.25f;
    Tower tower;

    void Awake()
    {
        tower = GetComponent<Tower>();
    }

    public GameObject SelectTarget()
    {
        // TODO: target enemy that's furthest along
        foreach (var enemy in EnemyManager.instance.enemies)
            if (enemy != null && IsInRange(enemy))
                return enemy;
        return null;
    }

    public bool IsInRange(GameObject target)
    {   
        /*
        Calculate distance between tower and target ignoring y difference.
        If the distance is less than the tower's range, we're in business!
        */
        // TODO: make better
        // PROBLEM: object centers aren't being considered v well

        // only target visible enemies
        if (!target.GetComponent<EnemyController>().IsVisible())
            return false;

        // find distance from tower to target, ignoring y-coordinate differences
        var targetPos = target.transform.position;
        var pos1 = new Vector3(transform.position.x, 0f, transform.position.z);
        var pos2 = new Vector3(targetPos.x, 0f, targetPos.z);
        var dist = Vector3.Distance(pos1, pos2);
        
        return dist <= (tower.rangeRadius + rangeBuffer);
    }
}
