using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpAmmo : MonoBehaviour
{
    //public int LightBullets = 0;
    //public int HeavyBullets = 0;
    //public int EnergyBullets = 0;
    //public int ExtraBullets = 0;
    public bool Weapon; 
    public int WeaponClass = 0; // 0==нихуя, 1 = Rifle, 2 = Shotgun, 3 = Rocket, 4 = Ripper,5 = PlasmaGun, 6 = Railgun, 10 = BFG;
    public int[] AmmoValue = { 0, 50, 50, 25, 25, 50, 50 };
    public GameObject VFX;
    public GameObject SFX;
    public float CD;
    float timer;


    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            GameObject weapons = other.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(3).GetChild(0).gameObject;

            GameObject vfx = Instantiate(VFX, transform.position, transform.rotation);
            GameObject sfx = Instantiate(SFX, transform.position, transform.rotation);
            Destroy(vfx, 2);
            Destroy(sfx, 2);

            if (WeaponClass > 0)
            {
                if (Weapon)
                {
                    //Если поднял НЕ такую же пушку
                    if (weapons.transform.GetChild(WeaponClass).gameObject.active == false)
                    {
                        for (int i = 0; i < weapons.transform.childCount; i++)
                        {
                            weapons.transform.GetChild(i).gameObject.SetActive(false);
                        }
                        weapons.transform.GetChild(WeaponClass).gameObject.SetActive(true);
                        weapons.transform.GetChild(WeaponClass).GetComponent<Weapons>().Ammo = 0;
                    }
                    //Добавляем патроны
                    weapons.transform.GetChild(WeaponClass).GetComponent<Weapons>().Ammo += AmmoValue[WeaponClass];
                }
                else
                {

                }
            }


            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            GetComponent<BoxCollider>().enabled = false;
            timer = CD;
        }   
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
