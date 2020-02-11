using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tactical;

public class AIShootable : Shootable
{
    [SerializeField] private WeaponController weaponController;
    [SerializeField] protected BaseCharacterController character;

    [SerializeField] private float scatter = 0;
    public GameObject bulletPrefab;
    public float damage = 0.5f;
    public Transform shootPos;

    public override void Attack(Vector3 targetPosition)
    {
        if (character.stats.IsAlive() && character.aiming)
        {
            if (weaponController.currentWeapon.multiShot)
            {
                int[] pool = { -5, -2, 0, 2, 5 };
                for (int i = 0; i < 5; i++)
                {
                    shootPos.localRotation = Quaternion.Euler(Random.Range(-scatter, scatter), Random.Range(-scatter, scatter) + pool[i], 0);
                    CreateBullet();
                }
            }
            else
            {
                shootPos.localRotation = Quaternion.Euler(Random.Range(-scatter, scatter), Random.Range(-scatter, scatter), 0);
                CreateBullet();
            }
            lastAttackTime = Time.time;
        }        
    }
    

    protected void CreateBullet()
    {
        weaponController.currentWeapon.PlayShotSound();

        GameObject newMuzzleFlash = MF_AutoPool.Spawn(weaponController.currentWeapon.muzzleFlash, 0, weaponController.currentWeapon.muzzleFlashPos.position,
            weaponController.currentWeapon.muzzleFlashPos.rotation);
        newMuzzleFlash.GetComponent<MuzzleFlash>().weaponController = weaponController;

        GameObject newBullet = MF_AutoPool.Spawn(bulletPrefab, shootPos.position, shootPos.rotation);
        newBullet.GetComponent<BulletController>().weaponController = this.weaponController;
        newBullet.GetComponent<BulletController>().isEnemy = true;
        newBullet.GetComponent<BulletController>().damage = this.damage;
    }
}
