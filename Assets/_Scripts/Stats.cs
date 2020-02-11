using BehaviorDesigner.Runtime.Tactical;
using MoreMountains.Feedbacks;
using MoreMountains.NiceVibrations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour, IDamageable
{
    [SerializeField] private MMFeedbacks damageFeedback;

    // здесь перечисляем только статы персонажа
    [Header("Main Stats")]
    [SerializeField] private bool isPlayer;
    public float health;
    public float currentHealth;

    [Header("Player Speed Stats")]
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
        if (!IsAlive())
        {
            return;
        }
        
        if (isPlayer)
        {
            MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
        }

        currentHealth -= amount;
        damageFeedback.PlayFeedbacks();
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
