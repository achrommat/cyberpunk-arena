using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tactical;

public class AIShootable : Shootable
{
    [SerializeField] private WeaponController weaponController;
    [SerializeField] protected BaseCharacterController character;
    // перенести в weapons
    public GameObject bulletPrefab;
    public float damage = 0.5f;
    public Transform shootPos;

    public override void Attack(Vector3 targetPosition)
    {
        if (character.stats.IsAlive() && character.aiming)
        {
            CreateBullet();
            lastAttackTime = Time.time;
        }        
    }

    protected void CreateBullet()
    {
        weaponController.currentWeapon.PlayShotSound();

        GameObject newBullet = MF_AutoPool.Spawn(bulletPrefab, shootPos.position, shootPos.rotation);
        newBullet.GetComponent<BulletController>().weaponController = this.weaponController;
        newBullet.GetComponent<BulletController>().isEnemy = true;
        newBullet.GetComponent<BulletController>().damage = this.damage;


        /*GameObject newbullet = bulletPool.transform.GetChild(0).gameObject;
        newbullet.GetComponent<BaseEnemyBulletController>().damage = this.damage;
        newbullet.transform.position = shootPos.transform.position;
        newbullet.transform.rotation = shootPos.transform.rotation;
        newbullet.transform.SetParent(null);
        newbullet.SetActive(true);*/
    }
}
