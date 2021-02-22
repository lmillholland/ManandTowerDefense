using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("Gameplay Settings")]
    public float damage;
    public float fireRate;
    public float rangeRadius;

    [Header("Level Up Rates")]
    public float damageIncreasePct;
    public float fireRateIncreasePct;
    public float rangeRadiusIncreasePct;

    [Header("References")]
    public GameObject rangeIndicator;
    public GameObject rotater;
    TowerSelectTarget targetSelector;

    [Header("General Settings")]
    public float rotateSpeed = 6f;
    public float rangeToWorldScale = 20f;       // for visualizing range
    public float lockedOnAngleThreshold = 15f;  // angle that indicates rotater faces target
    
    // trackers
    int level = 1;
    GameObject target;
    float coolDownTimer = 0f;
    bool isLockedOntoTarget = false;

    // accessors
    public GameObject Target => target;

    void Awake()
    {
        targetSelector = GetComponent<TowerSelectTarget>();
    }

    void Update()
    {
        /*
        Only update if game isn't paused.

        (Separates logic into its own overrideable HandleUpdate function so inheritors don't
        have to recreate this pause logic.)
        */
        if (!LevelManager.instance.IsPaused)
            HandleUpdate();
    }

    public virtual void HandleUpdate()
    {        
        if (target == null)
        {
            // search for new target
            target = targetSelector.SelectTarget();
            isLockedOntoTarget = false; // forces us to rotate to new target before firing
        }
        else
        {
            if (targetSelector.IsInRange(target))
            {
                // rotate to target and shoot
                RotateTowardTarget();
                if (isLockedOntoTarget && coolDownTimer == 0)
                    Fire();
            }
            else
                // target escaped; search for new target
                target = null;
        }

        // always reduce coolDownTimer
        if (coolDownTimer != 0)
        {
            coolDownTimer -= Time.deltaTime * LevelManager.instance.FastForwardSpeed;
            coolDownTimer = Mathf.Max(coolDownTimer, 0);
        }
    }

    // ----------------------------------------------------------------
    // Click
    //
    public void Select()
    {
        // TODO: upgrade system
        Debug.Log("selected");
    }
    
    // ----------------------------------------------------------------
    // Hover
    //
    public void Hover()
    {
        // Visualize range radius
        var indicatorSize = (rangeRadius + 1) * rangeToWorldScale;
        var scaleY = rangeIndicator.transform.localScale.y;
        rangeIndicator.transform.localScale = new Vector3(indicatorSize, scaleY, indicatorSize);
        rangeIndicator.SetActive(true);
    }

    public void Unhover()
    {
        // Hide range radius visualizer
        rangeIndicator.SetActive(false);
    }

    // ----------------------------------------------------------------
    // Rotate
    //
    void RotateTowardTarget()
    {
        // find target direction
        Vector3 targetDirection = target.transform.position - rotater.transform.position;
        Vector3 newDirection = Vector3.RotateTowards(rotater.transform.forward, targetDirection, rotateSpeed * Time.deltaTime, 0f);
        
        // rotate rotater
        var newRotation = Quaternion.LookRotation(newDirection).eulerAngles;
        newRotation.x = 0f;
        newRotation.z = 0f;
        rotater.transform.rotation = Quaternion.Euler(newRotation);

        // determine if we've locked on yet
        var angleToTarget = Vector3.Angle(rotater.transform.forward, target.transform.position - rotater.transform.position);
        if (!isLockedOntoTarget && angleToTarget < lockedOnAngleThreshold)
            isLockedOntoTarget = true;
    }

    // ----------------------------------------------------------------
    // Fire
    //
    public virtual void Fire()
    {
        coolDownTimer = fireRate;
    }

    // ----------------------------------------------------------------
    // Level up
    //
    void LevelUp()
    {
        // TODO: make this "AssignLevel" and calc stats based on that
        // this becomes important when mutating later towers
        damage *= 1 + (damageIncreasePct / 100);
        fireRate *= 1 + (fireRateIncreasePct / 100);
        rangeRadius *= 1 + (rangeRadiusIncreasePct / 100);
    }
}
