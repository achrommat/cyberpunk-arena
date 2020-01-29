using UnityEngine;

public class PlayerShootable : MonoBehaviour
{
    [SerializeField]
    private Weapon currentWeapon;

    private float nextAttackTime;

    public void Update()
    {
        if (currentWeapon.shooting == true)
        {
            if (currentWeapon.laserTarget)
            {
                currentWeapon.laserTarget.SetActive(true);
            }
            Attack();
        }
        else
        {
            if (currentWeapon.laserTarget)
            {
                currentWeapon.laserTarget.SetActive(false);
            }
        }
    }

    public void Attack()
    {
        if (currentWeapon.energy > 0 && Time.time >= nextAttackTime)
        {
            if (currentWeapon.multiShot)
            {
                int[] pool = { -5, -2, 0, 2, 5 };
                for (int i = 0; i < 5; i++)
                {
                    currentWeapon.GetScatter(pool, i);
                    CreateBullet();
                }
            }
            else
            {
                currentWeapon.GetScatter();
                CreateBullet();
            }
            nextAttackTime = Time.time + currentWeapon.attackDelay;
        }
    }

    private void CreateBullet()
    {
        currentWeapon.source.PlayOneShot(currentWeapon.clip);
        GameObject newbullet = currentWeapon.bulletPool.transform.GetChild(0).gameObject;
        newbullet.GetComponent<Bullet>().damage = currentWeapon.damage;
        newbullet.transform.position = currentWeapon.shootPos.transform.position;
        newbullet.transform.rotation = currentWeapon.shootPos.transform.rotation;
        newbullet.transform.SetParent(null);
        newbullet.SetActive(true);
        currentWeapon.shootPos.transform.localRotation = Quaternion.Euler(0, 0, 0);
        currentWeapon.energy -= 1;

        if (currentWeapon.ripper == true)
        {
            currentWeapon.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
