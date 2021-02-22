using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatusType {
    Poison
}

public class StatusEffect
{
    // General
    public StatusType type;
    EnemyController enemy;

    // trackers; will be modified by specific status effect
    int numInflicts = 0;
    float timer = 0f;
    int totalNumInflicts;
    float inflictFrequency;

    // Poison effects
    float poisonInflictFrequency = .75f;
    int poisonNumInflicts = 6;
    

    public StatusEffect(StatusType type, EnemyController enemy)
    {
        // store type and enemy
        this.type = type;
        this.enemy = enemy;

        // prep variables based on status effect
        if (type == StatusType.Poison)
        {
            inflictFrequency = poisonInflictFrequency;
            totalNumInflicts = poisonNumInflicts;
        }
    }

    public void HandleUpdate()
    {
        // check if we're done and should cure status
        if (numInflicts >= totalNumInflicts)
        {
            enemy.CureStatus(type);
        }
        else
        {
            // increase timer until next inflict
            timer += Time.deltaTime;
            if (timer > inflictFrequency)
            {
                timer = 0;
                numInflicts++;
                InflictStatus();
            }
        }
    }

    void InflictStatus()
    {
        /*
        Apply status based on effect type.
        */
        if (type == StatusType.Poison)
        {
            var damage = enemy.maxHealth * .0625f;
            enemy.TakeDamage(damage);
        }
    }
}
