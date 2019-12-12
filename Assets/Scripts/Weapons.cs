using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public GameObject bullet;
    public int ActiveWeapon;
    public GameObject ShootPos;
    public GameObject ShootFX;
    public GameObject AudioSource;
    public float speed;
    public bool shooting;

    public float ShootCD= 1;
    float ShootTimer =0;

    public void Update()
    {
        if(ShootTimer>=0)
        {
            ShootTimer -= Time.deltaTime;
        }
        if (shooting == true && ShootTimer <= 0)
        {
            Shoot();
        }
    }
    public void Shoot()
    {
        GameObject newbullet = Instantiate(bullet, transform.position, transform.rotation);
        GameObject newAudioSource = Instantiate(AudioSource, transform.position, transform.rotation);
        Destroy(newbullet, 2);
        Destroy(newAudioSource, 2);
        ShootTimer = ShootCD;
    }
}
