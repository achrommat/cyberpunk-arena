using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Weapons : MonoBehaviour
{
    public GameObject bullet;
    public GameObject ShootPos;
    public GameObject ShootFX;
    public AudioSource Source;
    public AudioClip Clip;
    public float speed;
    public float scatter =0;
    public float BulletLifeTime =0.5f;
    public bool shooting;

    public float ShootCD= 1;
    protected float ShootTimer = 0;

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
        Source.PlayOneShot(Clip);
        ShootPos.transform.localRotation = Quaternion.Euler(Random.Range(-scatter, scatter), Random.Range(-scatter, scatter), 0);
        GameObject newbullet = Instantiate(bullet, ShootPos.transform.position, ShootPos.transform.rotation);
        //GameObject newAudioSource = Instantiate(AudioSource, transform.position, transform.rotation);

        Destroy(newbullet, BulletLifeTime);
        //Destroy(newAudioSource, 2);
        ShootTimer = ShootCD;
    }
}
