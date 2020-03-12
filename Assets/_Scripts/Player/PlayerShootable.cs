using BehaviorDesigner.Runtime.Tactical;
using UnityEngine;
using EZCameraShake;
using MoreMountains.NiceVibrations;

public class PlayerShootable : Shootable
{
    // отвечает за стрельбу игрока
    [SerializeField] private WeaponController weaponController;
    private float nextAttackTime;

    public override void Attack()
    {
        //if (weaponController.currentWeapon.currentEnergy > 0 && Time.time >= nextAttackTime)
        if(Time.time >= nextAttackTime)
        {
            if (weaponController.currentWeapon.multiShot)
            {
                int[] pool = { -5, -2, 0, 2, 5 };
                for (int i = 0; i < 5; i++)
                {
                    weaponController.currentWeapon.GetScatter(pool, i);
                    Shoot();
                }
            }
            else if (weaponController.currentWeapon.devotion)
            {
                weaponController.currentWeapon.GetScatter();
                weaponController.currentWeapon.HandleDevotion();
                Shoot();
            }
            else
            {
                weaponController.currentWeapon.GetScatter();
                Shoot();
            }
            nextAttackTime = Time.time + weaponController.currentWeapon.currentAttackDelay;

            CameraShaker.Instance.ShakeOnce(2.5f, 4f, .1f, .1f);

            MMVibrationManager.Haptic(HapticTypes.LightImpact);
        }
    }

    private void Shoot()
    {
        weaponController.currentWeapon.currentEnergy -= 1;
        weaponController.currentWeapon.PlayShotSound();

        GameObject newMuzzleFlash = MF_AutoPool.Spawn(weaponController.currentWeapon.muzzleFlash, 1, weaponController.currentWeapon.muzzleFlashPos.position,
            weaponController.currentWeapon.muzzleFlashPos.rotation);
        newMuzzleFlash.GetComponent<MuzzleFlash>().weaponController = weaponController;

        GameObject newBullet = MF_AutoPool.Spawn(weaponController.currentWeapon.bulletPrefab, weaponController.currentWeapon.shootPosition.position,
            weaponController.currentWeapon.shootPosition.rotation);
        newBullet.GetComponent<BulletController>().weaponController = this.weaponController;        
    }
}
