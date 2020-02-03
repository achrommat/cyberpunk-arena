using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tactical;

public class AIShootable : Shootable
{
    // перенести в weapons
    public GameObject bulletPool;
    public float damage = 0.5f;
    public GameObject shootPos;

    public override void Attack()
    {
        CreateBullet();
        lastAttackTime = Time.time;
    }

    protected void CreateBullet()
    {
        GameObject newbullet = bulletPool.transform.GetChild(0).gameObject;
        newbullet.GetComponent<BaseEnemyBulletController>().damage = this.damage;
        newbullet.transform.position = shootPos.transform.position;
        newbullet.transform.rotation = shootPos.transform.rotation;
        newbullet.transform.SetParent(null);
        newbullet.SetActive(true);
    }
}
