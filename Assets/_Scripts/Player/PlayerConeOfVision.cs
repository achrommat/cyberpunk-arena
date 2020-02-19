using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConeOfVision : MMConeOfVision
{
    [ReadOnly] public EnemyController NearestTarget;

    private float _minDistanceToTarget;

    protected override void ScanForTargets()
    {
        _lastScanTimestamp = Time.time;

        ClearTargets();

        _targetsWithinDistance = Physics.OverlapSphere(Center, VisionRadius, TargetMask);
        foreach (Collider collider in _targetsWithinDistance)
        {
            // проверка жива ли цель
            EnemyController enemyController = collider.GetComponent<EnemyController>();
            if (!enemyController.stats.IsAlive())
            {
                continue;
            }

            _target = collider.transform;
            _directionToTarget = (_target.position - Center).normalized;
            if (Vector3.Angle(Direction, _directionToTarget) < VisionAngle / 2f)
            {
                _distanceToTarget = Vector3.Distance(Center, _target.position);
                
                bool duplicate = false;
                foreach (Transform visibleTarget in VisibleTargets)
                {
                    if (visibleTarget == _target)
                    {
                        duplicate = true;
                    }
                }

                if ((!Physics.Raycast(Center, _directionToTarget, _distanceToTarget, ObstacleMask)) && !duplicate)
                {
                    // Получаем близжаюшую цель к игроку
                    if (_distanceToTarget < _minDistanceToTarget)
                    {
                        if (NearestTarget)
                        {
                            NearestTarget.HideHighlightOnPlayerAiming();
                        }
                        _minDistanceToTarget = _distanceToTarget;
                        NearestTarget = enemyController;
                        NearestTarget.ShowHighlightOnPlayerAiming();
                    }

                    VisibleTargets.Add(_target);
                }
            }
        }
    }

    protected override void LateUpdate()
    {
        if ((Time.time - _lastScanTimestamp > ScanFrequencyInSeconds) && ShouldScanForTargets)
        {
            ScanForTargets();
        }
        DrawMesh();

        // отключаем конус, когда не целимся
        VisionMeshFilter.gameObject.SetActive(ShouldScanForTargets);
        if (!ShouldScanForTargets)
        {
            ClearTargets();
        }
    }

    protected void ClearTargets()
    {
        VisibleTargets.Clear();
        _minDistanceToTarget = float.MaxValue;
        if (NearestTarget)
        {
            NearestTarget.HideHighlightOnPlayerAiming();
            NearestTarget = null;
        }
    }
}
