using BehaviorDesigner.Runtime.Tactical;
using MoreMountains.Feedbacks;
using MoreMountains.NiceVibrations;
using UnityEngine;

public class Stats : MonoBehaviour, IDamageable
{
    [SerializeField] private MMFeedbacks damageFeedback;

    // здесь перечисляем только статы персонажа
    [Header("Main Stats")]
    [SerializeField] private bool _isPlayer;
    [SerializeField] private PlayerController _player;
    public float Health = 4;
    public float CurrentHealth;
    public float Accuracy = 1;
    public float BulletSpeed = 1;

    [Header("Player Speed Stats")]
    public float CurrentRunSpeed = 6;
    public float RunSpeed = 6;
    [SerializeField] private float _aimingPenalty = 2;

    public void Awake()
    {
        CurrentHealth = Health;
    }

    public void Damage(float amount)
    {
        if (!IsAlive())
        {
            return;
        }
        
        if (_isPlayer)
        {
            MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
        }

        CurrentHealth -= amount;
        damageFeedback.PlayFeedbacks();
    }

    public bool IsAlive()
    {
        return CurrentHealth > 0;
    }

    // Update is called once per frame
    void Update()
    {
        CurrentRunSpeed = (_player && _player.aiming) ? (RunSpeed - _aimingPenalty) : RunSpeed;
    }
}
