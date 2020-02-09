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
        if (weaponController.currentWeapon.currentEnergy > 0 && Time.time >= nextAttackTime)
        {
            if (weaponController.currentWeapon.multiShot)
            {
                int[] pool = { -5, -2, 0, 2, 5 };
                for (int i = 0; i < 5; i++)
                {
                    weaponController.currentWeapon.GetScatter(pool, i);
                    CreateBullet();
                }
            }
            else
            {
                weaponController.currentWeapon.GetScatter();
                CreateBullet();
            }
            nextAttackTime = Time.time + weaponController.currentWeapon.attackDelay;

            CameraShaker.Instance.ShakeOnce(2f, 3f, .1f, .2f);
            MMVibrationManager.Vibrate();
        }
    }

    private void CreateBullet()
    {
        weaponController.currentWeapon.PlayShotSound();
        GameObject newBullet = MF_AutoPool.Spawn(weaponController.currentWeapon.bulletPrefab, weaponController.currentWeapon.shootPos.position, weaponController.currentWeapon.shootPos.rotation);
        newBullet.GetComponent<BulletController>().weaponController = this.weaponController;
        //newBullet.GetComponent<BoxCollider>().enabled = true;
        weaponController.currentWeapon.currentEnergy -= 1;

        if (weaponController.currentWeapon.ripper == true)
        {
            weaponController.currentWeapon.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
