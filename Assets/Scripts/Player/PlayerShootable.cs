using BehaviorDesigner.Runtime.Tactical;
using UnityEngine;

public class PlayerShootable : Shootable
{
    // отвечает за стрельбу игрока
    [SerializeField]
    private WeaponController weaponController;

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
        }
    }

    private void CreateBullet()
    {
        weaponController.currentWeapon.PlayShotSound();
        Bullet newBullet = weaponController.currentWeapon.bulletPool.transform.GetChild(0).gameObject.GetComponent<Bullet>();        
        newBullet.weaponController = this.weaponController;
        newBullet.transform.SetParent(null);
        newBullet.gameObject.SetActive(true);
        weaponController.currentWeapon.shootPos.transform.localRotation = Quaternion.Euler(0, 0, 0);
        weaponController.currentWeapon.currentEnergy -= 1;

        if (weaponController.currentWeapon.ripper == true)
        {
            weaponController.currentWeapon.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
