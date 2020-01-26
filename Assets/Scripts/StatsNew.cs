using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tactical;

public class StatsNew : MonoBehaviour, IDamageable
{
    public float health;
    public float currentHealth;

    private void Start()
    {
        currentHealth = health;
    }

    public void Damage(float amount)
    {
        currentHealth -= amount;
    }

    public bool IsAlive()
    {
        return currentHealth > 0;
    }
}
