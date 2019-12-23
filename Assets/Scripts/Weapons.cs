using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Weapons : MonoBehaviour
{
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

    public float ShootCD= 1;
    protected float ShootTimer = 0;

    public void Update()
    {
        if(ShootTimer>=0)
        {
            ShootTimer -= Time.deltaTime;
        }

        if (ShootTimer <= 0)
        {
            if (Ripper == true)
            {
                transform.GetChild(0).gameObject.SetActive(true);
            }
            if (shooting == true)
            {
                Shoot();
                LaserTarget.SetActive(true);
            }
            //else
            //{
            //    LaserTarget.SetActive(false);
            //}
        }    
    }
    public void Shoot()
    {
        if(Ammo > 0)
        {
            if (multishot == true)
            {
                Source.PlayOneShot(Clip);
                ShootPos.transform.localRotation = Quaternion.Euler(Random.Range(-scatter, scatter), Random.Range(-scatter, scatter), 0);
                //GameObject newbullet1 = Instantiate(bullet, ShootPos.transform.position, ShootPos.transform.rotation);
                //        ShootPos.transform.localRotation = Quaternion.Euler(Random.Range(-scatter, scatter), Random.Range(-scatter, scatter)-25, 0);
                //        GameObject newbullet2 = Instantiate(bullet, ShootPos.transform.position, ShootPos.transform.rotation);
                //         Destroy(newbullet1, BulletLifeTime);
                //        Destroy(newbullet2, BulletLifeTime);
                int[] pool = { -20, -10, 0, 20, 10 };
                for (int i = 0; i < 5; i++)
                {
                    ShootPos.transform.localRotation = Quaternion.Euler(Random.Range(-scatter, scatter), Random.Range(-scatter, scatter) + pool[i], 0);
                    GameObject newbullet = Instantiate(bullet, ShootPos.transform.position, ShootPos.transform.rotation);
                    Destroy(newbullet, BulletLifeTime);
                    ShootPos.transform.localRotation = Quaternion.Euler(0, 0, 0);
                    Ammo -= 1;
                }

            }

            else
            {
                Source.PlayOneShot(Clip);
             //   ShootPos.transform.localRotation = Quaternion.Euler(Random.Range(-scatter, scatter), Random.Range(-scatter, scatter), 0);
                GameObject newbullet = Instantiate(bullet, ShootPos.transform.position, ShootPos.transform.rotation);
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
