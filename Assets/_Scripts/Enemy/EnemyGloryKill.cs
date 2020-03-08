using EZCameraShake;
using MoreMountains.Feedbacks;
using MoreMountains.NiceVibrations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGloryKill : MonoBehaviour
{
    [SerializeField] private GameObject _gore;
    [SerializeField] private MMFeedbacks _gloryKillFeedback;
    [SerializeField] private AP_Reference _gorePoolRef;
    [SerializeField] private AP_Reference _enemyPoolRef;
    [SerializeField] private bool _gloryKill = false;    

    void Update()
    {
        if (_gloryKill)
        {
            GloryKill();
        }
    }

    public void GloryKill()
    {
        gameObject.SetActive(false);
        GameManager.instance.AliveEnemyCount--;
        GameManager.instance.AddScore(100);
        MMVibrationManager.Haptic(HapticTypes.MediumImpact);
        CameraShaker.Instance.ShakeOnce(2.5f, 5f, .1f, .1f);
        MF_AutoPool.Spawn(_gore, transform.position, transform.rotation);
        _gloryKillFeedback.PlayFeedbacks();
        MF_AutoPool.Despawn(_gorePoolRef, 2f);
        MF_AutoPool.Despawn(_enemyPoolRef, 2f);
    }
}
