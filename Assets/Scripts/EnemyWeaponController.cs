using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class EnemyWeaponController : MonoBehaviour
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
    public float scatter = 0;
    public float BulletLifeTime = 0.5f;
    public bool shooting;
    public int Ammo;
    public bool Ripper;
    public GameObject BulletPool;

    public float ShootCD = 1;
    protected float ShootTimer = 0;

    public void Shoot()
    {
        
    }
}
