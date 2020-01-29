using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Weapons : MonoBehaviour
{
    // TODO: нахер убрать лишние поля из скрипта. оставить ссылку на оружие, ссылку на пул переменную урона, скорости пули, булевый мультишот.
    // здесь прописываем только статы оружия. стрельбу делать из Shootable, как это сделано сейчас в AIShootable
    [Header("Settings")]
    public GameObject bullet;
    public GameObject bulletPool;
    public GameObject laserTarget;
    public GameObject shootPos;
    public GameObject shootFX;
    public AudioSource source;

    [Header("Main Stats")]
    public float damage;
    public float speed;
    public bool multiShot;
    public int ammo;
    public float energy;

    [Header("Fire Rate")]
    public float attackDelay = 1;
    protected float nextAttackTime = 0;

    public AudioClip clip;
    
    public float scatter = 0;
    public float bulletLifeTime = 0.5f;
    public bool shooting;
    
    public bool ripper;    
 
    public void Update()
    {
        if (shooting == true)
        {
            if (laserTarget)
            {
                laserTarget.SetActive(true);
            }
            Attack();
        }
        else
        {
            if (laserTarget)
            {
                laserTarget.SetActive(false);
            }
        }
    }

    public void Attack()
    {
        if(energy > 0 && Time.time >= nextAttackTime)
        {            
            if (multiShot)
            {
                int[] pool = { -5, -2, 0, 2, 5 };
                for (int i = 0; i < 5; i++)
                {
                    CreateBullet();
                }                    
            }
            else
            {
                CreateBullet();
            }
            nextAttackTime = Time.time + attackDelay;
        }
    }

    private void CreateBullet()
    {
        source.PlayOneShot(clip);
        GameObject newbullet = bulletPool.transform.GetChild(0).gameObject;
        newbullet.GetComponent<Bullet>().damage = this.damage;
        newbullet.transform.position = shootPos.transform.position;
        newbullet.transform.rotation = shootPos.transform.rotation;
        newbullet.transform.SetParent(null);
        newbullet.SetActive(true);
        shootPos.transform.localRotation = Quaternion.Euler(0, 0, 0);
        energy -= 1;

        if (ripper == true)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
