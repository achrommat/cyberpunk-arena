using BehaviorDesigner.Runtime.Tactical;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour, IDamageable
{
    // здесь перечисляем только статы персонажа
    [Header("Main Stats")]
    public float health;
    public float currentHealth;


    // пока не понял зачем это
    [Header("Player Stats")]
    public float runSpeed;
    public float startRunSpeed;
    public float speedWithAim;
    public float runSpeedMody;
    //public float speedaim;
    public float speedsetting = 1;

    public void Awake()
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

    // Update is called once per frame
    void Update()
    {
        runSpeed = (startRunSpeed + speedWithAim + runSpeedMody);
    }
}
