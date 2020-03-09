using EZCameraShake;
using MoreMountains.Feedbacks;
using MoreMountains.NiceVibrations;
using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] private PlayerController _player;

    [Header("Dash")]
    [SerializeField] private float _dashDistance = 15f;
    [SerializeField] private float _cooldown = 1f;
    [SerializeField] private float _dashDuration = 0.75f;
    [SerializeField] private GameObject _dashVFX;
    [SerializeField] private Transform _dash;
    [SerializeField] private AP_Reference _dashRefPool;
    private GameObject newDash;

    private bool _canDash = true;

    private float _touchCooldown = 0.5f;
    private int _touchCount = 0;

    private int _touchNeeded = 2;

    void Update()
    {
       // HandleInput();
    }

    /*private void HandleInput()
    {
#if UNITY_EDITOR
        _touchNeeded = 1;
#endif

        if (Input.GetButtonDown("Fire1"))
        {
            if (_touchCooldown > 0 && _touchCount == 1)
            {
                DashStart();
            }
            else
            {
                _touchCooldown = 0.5f;
                _touchCount += 1;
            }
        }
        if (_touchCooldown > 0)
        {
            _touchCooldown -= 1 * Time.deltaTime;
        }
        else
        {
            _touchCount = 0;
        }
    }*/

    public void DashStart()
    {
        if (_canDash)
        {
            _player.rb.velocity = Vector3.zero;
            _player.Dash = true;
            _player.rb.AddForce(transform.forward * _dashDistance, ForceMode.Impulse);
            newDash = MF_AutoPool.Spawn(_dashVFX, _dash.position, _dash.rotation);
            MMVibrationManager.Haptic(HapticTypes.LightImpact);
            CameraShaker.Instance.ShakeOnce(4f, 6f, .1f, .1f);
            StartCoroutine(DashStop());
            StartCoroutine(Cooldown());
        }
    }

    private IEnumerator DashStop()
    {
        yield return new WaitForSeconds(_dashDuration);
        _player.rb.velocity = Vector3.zero;
        _player.Dash = false;
        MF_AutoPool.Despawn(newDash);
    }

    private IEnumerator Cooldown()
    {
        _canDash = false;
        yield return new WaitForSeconds(_cooldown);
        _canDash = true;
    }
}
