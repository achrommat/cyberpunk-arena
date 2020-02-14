﻿using MoreMountains.TopDownEngine;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Настройки оружия
    [Header("Main Stats")]
    public new string name = "Weapon Name";
    public float damage = 1;
    public float bulletForce = 1;
    public bool multiShot = false;
    [SerializeField] private float maxEnergy = 100;

    [HideInInspector] public float currentEnergy;

    // TODO: убрать
    public float ammo = 100;

    [Header("Fire Rate")]
    public float attackDelay = 1;

    [Header("Settings")]
    public GameObject bulletPrefab;
    public GameObject laserTarget;
    public Transform shootPos;

    [Header("FX")]
    public WeaponLaserSight laserSight;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private float scatter = 0;
    public bool shooting;

    public GameObject muzzleFlash;
    public Transform muzzleFlashPos;

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

    public void PlayShotSound()
    {
        audioSource.PlayOneShot(audioSource.clip);
    }
}
