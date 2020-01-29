﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Weapon : MonoBehaviour
{
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

    public AudioClip clip;
    
    public float scatter = 0;
    public float bulletLifeTime = 0.5f;
    public bool shooting;
    
    public bool ripper;    
 
    public void GetScatter(int[] pool, int i)
    {
        shootPos.transform.localRotation = Quaternion.Euler(Random.Range(-scatter, scatter), Random.Range(-scatter, scatter) + pool[i], 0);
    }

    public void GetScatter()
    {
        shootPos.transform.localRotation = Quaternion.Euler(Random.Range(-scatter, scatter), Random.Range(-scatter, scatter), 0);
    }
}
