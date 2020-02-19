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
        _coneOfVision.ShouldScanForTargets = _playerController.CanShoot;
        ShootAtNearestTarget();
    }

    private void ShootAtNearestTarget()
    {
        if (!_coneOfVision.NearestTarget)
        {
            return;
        }

        if (_playerController.CanShoot)
        {
            if (_coneOfVision.NearestTarget)
            {
                aimPoint.LookAt(_coneOfVision.NearestTarget.myCollider.bounds.center);
                _playerController.shootable.Attack();
            }
        }
    }
}
