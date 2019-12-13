using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpAmmo : MonoBehaviour
{
    public int LightBullets = 0;
    public int HeavyBullets = 0;
    public int EnergyBullets = 0;
    public int ExtraBullets = 0;
    public GameObject VFX;
    public GameObject SFX;
    public float CD;
    float timer;


    public void OnTriggerEnter(Collider other)
    {
        GameObject weapons = other.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(3).GetChild(0).gameObject;
        weapons.transform.GetChild(1).GetComponent<Weapons>().Ammo += LightBullets;
        weapons.transform.GetChild(2).GetComponent<Weapons>().Ammo += EnergyBullets;
        weapons.transform.GetChild(3).GetComponent<Weapons>().Ammo += LightBullets;
        weapons.transform.GetChild(4).GetComponent<Weapons>().Ammo += LightBullets;
        weapons.transform.GetChild(5).GetComponent<Weapons>().Ammo += HeavyBullets;
        weapons.transform.GetChild(6).GetComponent<Weapons>().Ammo += ExtraBullets;
        GameObject vfx = Instantiate(VFX, transform.position,transform.rotation);
        GameObject sfx = Instantiate(SFX, transform.position, transform.rotation);
        Destroy(vfx, 2);
        Destroy(sfx, 2);
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        GetComponent<BoxCollider>().enabled = false;
        timer = CD;
    }
    public void FixedUpdate()
    {
        if (timer > 0)
            timer -= Time.fixedDeltaTime;
        else
        {
            GetComponent<BoxCollider>().enabled = true;
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
