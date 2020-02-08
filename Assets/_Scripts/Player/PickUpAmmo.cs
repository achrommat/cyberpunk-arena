using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpAmmo : MonoBehaviour
{
    public bool Weapon;
    public int WeaponClass = 0; // 0==нихуя, 1 = Rifle, 2 = Shotgun, 3 = Rocket, 4 = Ripper,5 = PlasmaGun, 6 = Railgun, 7 = BFG;
    public int AmmoClass = 0; // 0 == нихуя, 1 = Light, 2 = Extra, 3 = Energy
    public int Health;
    public int Armor;
    private int[] AmmoValue = { 0, 50, 50, 25, 25, 50, 50 }; // Кол-во патронов с пака для разных пушек
    public GameObject VFX;
    public GameObject SFX;
    GameObject weapons;
    public float CD;
    float timer;


    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Health == 0 && Armor == 0)
            {
                weapons = other.transform.GetChild(0).GetComponent<CharController>().Weapons;
                GameObject vfx = Instantiate(VFX, transform.position, transform.rotation);
                // GameObject sfx = Instantiate(SFX, transform.position, transform.rotation);
                Destroy(vfx, 2);
                //  Destroy(sfx, 2);
                GetComponent<AudioSource>().Play();
                if (Weapon && WeaponClass > 0)
                {
                    //Если поднял НЕ такую же пушку
                    if (weapons.transform.GetChild(WeaponClass).gameObject.active == false)
                    {
                        for (int i = 0; i < weapons.transform.childCount; i++)
                        {
                            weapons.transform.GetChild(i).gameObject.SetActive(false);
                        }
                        weapons.transform.GetChild(WeaponClass).gameObject.SetActive(true);
                        weapons.transform.GetChild(WeaponClass).GetComponent<Weapon>().ammo = 0;
                    }
                    //Добавляем патроны
                    weapons.transform.GetChild(WeaponClass).GetComponent<Weapon>().ammo += AmmoValue[WeaponClass];
                }
                if (Weapon == false && AmmoClass > 0)
                {
                    if (AmmoClass == 1)
                    {
                        weapons.transform.GetChild(1).GetComponent<Weapon>().ammo += AmmoValue[1];
                        weapons.transform.GetChild(2).GetComponent<Weapon>().ammo += AmmoValue[2];
                    }
                    if (AmmoClass == 2)
                    {
                        weapons.transform.GetChild(3).GetComponent<Weapon>().ammo += AmmoValue[3];
                        weapons.transform.GetChild(4).GetComponent<Weapon>().ammo += AmmoValue[4];
                    }
                    if (AmmoClass == 3)
                    {
                        weapons.transform.GetChild(5).GetComponent<Weapon>().ammo += AmmoValue[5];
                        weapons.transform.GetChild(6).GetComponent<Weapon>().ammo += AmmoValue[6];
                    }
                }
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
                GetComponent<BoxCollider>().enabled = false;
                timer = CD;

            }
            if ((other.transform.GetChild(0).GetComponent<CharController>().Health < 4 && Health > 0) || (other.transform.GetChild(0).GetComponent<CharController>().Armor < 100 && Armor > 0))
            {
                GameObject vfx = Instantiate(VFX, transform.position, transform.rotation);
                Destroy(vfx, 2);
                GetComponent<AudioSource>().Play();
                other.transform.GetChild(0).GetComponent<CharController>().Health += Health;
                if (other.transform.GetChild(0).GetComponent<CharController>().Health > 4)
                {
                    other.transform.GetChild(0).GetComponent<CharController>().Health = 4;
                }
                other.transform.GetChild(0).GetComponent<CharController>().Armor += Armor;
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
                GetComponent<BoxCollider>().enabled = false;
                timer = CD;
            }
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
