using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeballTower : Tower
{
    [Header("Eyeball Tower References")]
    public GameObject eyeball;
    LineRenderer laserLine;
    LayerMask remainsLayer;

    void Start()
    {
        laserLine = eyeball.GetComponent<LineRenderer>();
        remainsLayer = LayerMask.NameToLayer("Remains");
    }

    public override void Fire()
    {   
        /*
        Visualize laser beam and deal damage.
        */
        if (Target != null)
        {
            Target.GetComponent<EnemyController>().TakeDamage(damage);
            laserLine.enabled = true;
        }
        base.Fire();
    }

    public override void HandleUpdate()
    {
        /*
        Set coords for laser beam.
        */
        if (Target != null)
        {
            // set start point
            laserLine.SetPosition(0, eyeball.transform.position);

            // set end position
            Vector3 rayOrigin = eyeball.transform.position;
            var direction = (Target.transform.position - eyeball.transform.position).normalized;
            RaycastHit hit;
            var length = rangeRadius * rangeToWorldScale;

            // check if we can hit target
            if (Physics.Raycast(rayOrigin, direction, out hit, length, ~remainsLayer))
                laserLine.SetPosition(1, hit.point);
            else
                laserLine.enabled = false;
        }
        else
        {
            laserLine.enabled = false;
        }
        base.HandleUpdate();
    }
}
