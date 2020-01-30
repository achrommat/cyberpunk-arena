using UnityEngine;

public class PlayerShootable : MonoBehaviour
{
    [SerializeField]
    private WeaponController weaponController;

    private float nextAttackTime;

    public void Update()
    {
        if (weaponController.currentWeapon.shooting == true)
        {
            if (weaponController.currentWeapon.laserTarget)
            {
                weaponController.currentWeapon.laserTarget.SetActive(true);
            }
            Attack();
        }
        else
        {
            if (weaponController.currentWeapon.laserTarget)
            {
                weaponController.currentWeapon.laserTarget.SetActive(false);
            }
        }
    }

    public void Attack()
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
        weaponController.currentWeapon.source.PlayOneShot(weaponController.currentWeapon.clip);
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
