using EZCameraShake;
using MoreMountains.Feedbacks;
using MoreMountains.NiceVibrations;
using System.Collections;
using UnityEngine;

public class EnemyGloryKill : MonoBehaviour
{
    [SerializeField] private EnemyController _enemy;
    [SerializeField] private Collider _collider;
    [SerializeField] private GameObject _enemyMesh;
    [SerializeField] private GameObject _gore;
    [SerializeField] private MMFeedbacks _gloryKillFeedback;
    [SerializeField] private AP_Reference _enemyPoolRef;
    private GameObject _newGore;

    public void GloryKill()
    {
        _gloryKillFeedback.PlayFeedbacks();
        _enemyMesh.SetActive(false);
        _collider.enabled = false;
        //GameManager.instance.AliveEnemyCount--;
        GameManager.instance.AddScore(100);
        MMVibrationManager.Haptic(HapticTypes.MediumImpact);
        CameraShaker.Instance.ShakeOnce(10f, 10f, .1f, .5f);
        _newGore = MF_AutoPool.Spawn(_gore, transform.position, transform.rotation);
        StartCoroutine(Despawn());
        _enemy.stats.CurrentHealth = 0;
    }

    private IEnumerator Despawn()
    {
        yield return new WaitForSeconds(2);
        MF_AutoPool.Despawn(_newGore);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && _enemy._isStunned)
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player.Dash)
            {
                GloryKill();                
            }
            return;
        }
    }
}
