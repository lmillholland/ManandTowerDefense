using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyController : MonoBehaviour
{
    [Header("Stats")]
    public int level = 1;
    public float maxHealth;
    public float speed;
    public float damage;
    public int money = 10;

    [Header("Stat Growth")]
    public float healthIncreasePct;
    public float speedIncreasePct;
    public float damageIncreasePct;

    [Header("Prefabs")]
    public GameObject remainsPrefab;

    // references
    HUDController hud;
    EnemyMoveController moveController;
    GameObject remains;
    Renderer renderer;

    // status trackers
    bool isDead;
    [HideInInspector] public bool canHurtCastle = true;
    List<StatusEffect> statuses = new List<StatusEffect>();

    void Awake()
    {
        moveController = GetComponent<EnemyMoveController>();
        hud = GetComponentInChildren<HUDController>();
        renderer = GetComponentInChildren<Renderer>();
    }

    public void Initialize(int enemyLevel)
    {
        /*
        Set stats based on level.
        */
        level = enemyLevel;

        // update stats based on level
        maxHealth *= Mathf.Pow(healthIncreasePct/100 + 1, level - 1);
        speed *= Mathf.Pow(speedIncreasePct/100 + 1, level - 1);
        damage *= Mathf.Pow(damageIncreasePct/100 + 1, level - 1);

        // double money per level
        money *= (int) Mathf.Pow(2, level - 1);

        // initialize HUD
        hud.Initialize(level, maxHealth);
    }

    void Update()
    {
        /*
        Update status effects and movement.
        */
        if (LevelManager.instance.IsPaused)
            return;
        foreach (var status in statuses)
            status.HandleUpdate();
        moveController.HandleUpdate();
    }

    public void TakeDamage(float damage)
    {
        hud.TakeDamage(damage);
    }

    public virtual void Die(bool crashedIntoCastle=false)
    {
        /*
        Award player money if a tower killed this enemy; if it crashed into the Castle,
        the players gets nothing.
        */
        if (!crashedIntoCastle)
        {
            // award player money and spawn interior enemies
            ResourceManager.instance.GainMoney(money);
        }

        // destruction
        EnemyManager.instance.RemoveEnemy(gameObject);
        remains = Instantiate(remainsPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void InflictStatus(StatusType statusType)
    {
        /*
        Inflict status on this enemy.

        Records this new status in a list and adds a HUD icon.
        */
        bool hasStatus = statuses.Where(x => x.type == statusType).Any();
        if (!hasStatus)
        {
            statuses.Add(new StatusEffect(statusType, this));
            hud.AddStatusEffect(statusType);
        }
    }

    public void CureStatus(StatusType statusType)
    {
        /*
        Remove status from this enemy.
        */
        statuses = statuses.Where(x => x.type != statusType).ToList();
        hud.RemoveStatusEffect(statusType);
    }

    public bool IsVisible()
    {
        return renderer.isVisible;
    }
}