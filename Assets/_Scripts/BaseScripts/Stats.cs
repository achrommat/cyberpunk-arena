using BehaviorDesigner.Runtime.Tactical;
using MoreMountains.Feedbacks;
using MoreMountains.NiceVibrations;
using UnityEngine;
using UnityEngine.Events;

public class Stats : MonoBehaviour, IDamageable
{
    [SerializeField] private MMFeedbacks damageFeedback;

    // здесь перечисляем только статы персонажа
    [Header("Main Stats")]
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
        
        if (_player)
        {
            MMVibrationManager.Haptic(HapticTypes.HeavyImpact);

            if (HealthBarViewController.CurrentItem.Fill.fillAmount <= amount)
            {
                HealthBarViewController.CurrentItem.Fill.fillAmount -= amount;
                HealthBarViewController.CurrentItemIndex++;
                HealthBarViewController.SetCurrentItem();
            }
            else
            {
                HealthBarViewController.CurrentItem.Fill.fillAmount -= amount;
            }

            //_player.TemporaryInvulnerability();
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
