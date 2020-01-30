using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Weapon : MonoBehaviour
{
    [Header("Main Stats")]
    public new string name = "Weapon Name";
    public float damage = 1;
    public float force = 1;
    public float bulletLifetime = 1;
    public bool multiShot = false;
    public int ammo;
    public float maxEnergy = 100;
    public float currentEnergy;

    [Header("Fire Rate")]
    public float attackDelay = 1;

    [Header("Settings")]
    public GameObject bullet;
    public GameObject bulletPool;
    public GameObject laserTarget;
    public Transform shootPos;
    public GameObject shootFX;
    public AudioSource source;

    public AudioClip clip;
    
    public float scatter = 0;
    public float bulletLifeTime = 0.5f;
    public bool shooting;
    
    public bool ripper;

    private void OnEnable()
    {
        currentEnergy = maxEnergy;
    }

    public void GetScatter(int[] pool, int i)
    {
        shootPos.localRotation = Quaternion.Euler(Random.Range(-scatter, scatter), Random.Range(-scatter, scatter) + pool[i], 0);
    }

    public void GetScatter()
    {
        shootPos.localRotation = Quaternion.Euler(Random.Range(-scatter, scatter), Random.Range(-scatter, scatter), 0);
    }
}
