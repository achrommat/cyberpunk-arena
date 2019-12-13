using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int Health;
    public int Defence;
    public int LightBullets = 0;
    public int HeavyBullets = 0;
    public int EnergyBullets = 0;
    public int ExtraBullets = 0;
 
    public int ActiveWeapon;
   
    public GameObject weapons;

    public void AddAmmo()
    {
        weapons.transform.GetChild(1).GetComponent<Weapons>().Ammo += LightBullets;
        weapons.transform.GetChild(2).GetComponent<Weapons>().Ammo += EnergyBullets;
        weapons.transform.GetChild(3).GetComponent<Weapons>().Ammo += LightBullets;
        weapons.transform.GetChild(4).GetComponent<Weapons>().Ammo += LightBullets;
        weapons.transform.GetChild(5).GetComponent<Weapons>().Ammo += HeavyBullets;
        weapons.transform.GetChild(6).GetComponent<Weapons>().Ammo += ExtraBullets;



    }
}
