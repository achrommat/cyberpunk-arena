﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Weapons : MonoBehaviour
{


    // TODO: нахер убрать лишние поля из скрипта. оставить ссылку на оружие, ссылку на пул переменную урона, скорости пули, булевый мультишот.
    // здесь прописываем только статы оружия. стрельбу делать из Shootable, как это сделано сейчас в AIShhotable
    public GameObject bullet;
    public GameObject LaserTarget;
    public GameObject Player;
    public GameObject ShootPos;
    public GameObject ShootFX;
    public AudioSource Source;
    public bool multishot;
    public AudioClip Clip;
    public float speed;
    public float scatter =0;
    public float BulletLifeTime =0.5f;
    public bool shooting;
    public int Ammo;
    public bool Ripper;
    public GameObject BulletPool;

    public float ShootCD= 1;
    protected float ShootTimer = 0;
 
    public void Update()
    {
        if(ShootTimer>=0)
        {
            ShootTimer -= Time.deltaTime;
        }

        if (shooting == true)
        {
            LaserTarget.SetActive(true);
            if (ShootTimer <= 0)
            {
                if (Ripper == true)
                {
                    transform.GetChild(0).gameObject.SetActive(true);
                }
                Shoot();
            }
        }
        else
        {
            LaserTarget.SetActive(false);
        }
    }
    public void Shoot()
    {
        if(Ammo > 0)
        {
            if (multishot == true)
            {
                Source.PlayOneShot(Clip);
               // ShootPos.transform.localRotation = Quaternion.Euler(Random.Range(-scatter, scatter), Random.Range(-scatter, scatter), 0);
                //GameObject newbullet1 = Instantiate(bullet, ShootPos.transform.position, ShootPos.transform.rotation);
                //        ShootPos.transform.localRotation = Quaternion.Euler(Random.Range(-scatter, scatter), Random.Range(-scatter, scatter)-25, 0);
                //        GameObject newbullet2 = Instantiate(bullet, ShootPos.transform.position, ShootPos.transform.rotation);
                //         Destroy(newbullet1, BulletLifeTime);
                //        Destroy(newbullet2, BulletLifeTime);
                int[] pool = { -5, -2, 0, 2, 5 };
                for (int i = 0; i < 5; i++)
                {
                    ShootPos.transform.localRotation = Quaternion.Euler(Random.Range(-scatter, scatter), Random.Range(-scatter, scatter) + pool[i], 0);
                    //GameObject newbullet = Instantiate(bullet, ShootPos.transform.position, ShootPos.transform.rotation);
                    //Destroy(newbullet, BulletLifeTime);
                    GameObject newbullet = BulletPool.transform.GetChild(0).gameObject;
                    newbullet.transform.position = ShootPos.transform.position;
                    newbullet.transform.rotation = ShootPos.transform.rotation;
                    newbullet.transform.SetParent(null);
                    newbullet.SetActive(true);
                    ShootPos.transform.localRotation = Quaternion.Euler(0, 0, 0);
                    Ammo -= 1;
                }

            }

            else
            {
                Source.PlayOneShot(Clip);
                   ShootPos.transform.localRotation = Quaternion.Euler(Random.Range(-scatter, scatter), Random.Range(-scatter, scatter), 0);
               // GameObject newbullet = Instantiate(bullet, ShootPos.transform.position, ShootPos.transform.rotation);
                GameObject newbullet = BulletPool.transform.GetChild(0).gameObject;
                newbullet.transform.position = ShootPos.transform.position;
                newbullet.transform.rotation = ShootPos.transform.rotation;
                newbullet.transform.SetParent(null);
                newbullet.SetActive(true);

                //    Destroy(newbullet, BulletLifeTime);
                //ShootPos.transform.localRotation = Quaternion.Euler(0, 0, 0);
                Ammo -= 1;
                if (Ripper == true)
                {
                    transform.GetChild(0).gameObject.SetActive(false);
                }
            }
            //GameObject newAudioSource = Instantiate(AudioSource, transform.position, transform.rotation);

            //Destroy(newAudioSource, 2);
            ShootTimer = ShootCD;
        }
    }
}
