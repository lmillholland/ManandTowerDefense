using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomTower : Tower
{
    ParticleSystem spores;
    public StatusType statusEffect = StatusType.Poison;

    public void Start()
    {
        spores = GetComponentInChildren<ParticleSystem>();
    }

    public override void Fire()
    {   
        // Spread spore particle effect
        spores.startSpeed = GetSporeStartSpeed();
        spores.Play();
        base.Fire();
    }

    float GetSporeStartSpeed()
    {
        // Magic number warning: measurement determined via manual tests
        return (rangeRadius - 1) * 1.5f + 1f;
    }
}

