using BehaviorDesigner.Runtime.Tactical;
using UnityEngine;
using MoreMountains.TopDownEngine;
using MoreMountains.Tools;

public class PlayerAiming : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private PlayerConeOfVision _coneOfVision;
    [SerializeField] private Transform aimPoint;

    private void LateUpdate()
    {
        if (!_playerController.stats.IsAlive())
        {
            return;
        }
        _coneOfVision.ShouldScanForTargets = _playerController.CanShoot;
        Shoot();
    }

    private void Shoot()
    {
        if (_playerController.CanShoot)
        {
            if (_coneOfVision.NearestTarget)
            {
                aimPoint.LookAt(_coneOfVision.NearestTarget.myCollider.bounds.center);
            }
            else
            {
                aimPoint.localRotation = Quaternion.identity;
            }

            _playerController.shootable.Attack();
        }
    }
}
