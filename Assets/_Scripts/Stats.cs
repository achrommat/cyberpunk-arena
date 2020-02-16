using BehaviorDesigner.Runtime.Tactical;
using MoreMountains.Feedbacks;
using MoreMountains.NiceVibrations;
using UnityEngine;

public class Stats : MonoBehaviour, IDamageable
{
    [SerializeField] private MMFeedbacks damageFeedback;

    // здесь перечисляем только статы персонажа
    [Header("Main Stats")]
    [SerializeField] private bool isPlayer;
    [SerializeField] private PlayerController player;
    public float health;
    public float currentHealth;

    [Header("Player Speed Stats")]
    public float currentRunSpeed;
    public float runSpeed;
    [SerializeField] private float aimingPenalty;

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
        currentRunSpeed = (player && player.aiming) ? (runSpeed - aimingPenalty) : runSpeed;
    }
}
