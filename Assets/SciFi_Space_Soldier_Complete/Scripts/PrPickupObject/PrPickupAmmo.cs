using UnityEngine;
using System.Collections;

public class PrPickupAmmo : PrPickupObject {

    //public enum Ammo
    //{
    //    Small = 4,
    //    Medium = 2,
    //    Full = 1
    //}
    //[Header("Type Ammo Settings")]
    //public Ammo LoadType = Ammo.Full;
    public int Health = 25;
    public int riflebullets = 0;
    public int laserbullets = 0;
    public int rocketbullets = 0;
    public int pistolbullets = 0;
    public int shotgunbullets = 0;

    public float time = 0;
    public float cooldown = 5;
    public bool bullet;
    public bool heal;
    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0 && transform.GetComponent<BoxCollider>().enabled == false)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(2).gameObject.SetActive(true);
            transform.GetComponent<BoxCollider>().enabled = true;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && bullet == true)
        {
           // other.GetComponent<PlayerStuff>().riflebullets += Amount;
           //other.GetComponent<PlayerStuff>().weapons.transform.GetChild(1).GetComponent<PrWeapon>().ActualBullets += Amount;
           // other.GetComponent<PlayerStuff>().weapons.transform.GetChild(1).GetComponent<PrWeapon>().UpdateWeaponGUI();

            //other.GetComponent<PlayerStuff>().riflebullets += riflebullets;
            //other.GetComponent<PlayerStuff>().laserbullets += laserbullets;
            //other.GetComponent<PlayerStuff>().rocketbullets += rocketbullets;
            //other.GetComponent<PlayerStuff>().pistolbullets += pistolbullets;
            //other.GetComponent<PlayerStuff>().shotgunbullets += shotgunbullets;

            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(false);
            transform.GetComponent<BoxCollider>().enabled = false;
            time = cooldown;

        }
        if (other.CompareTag("Player") && heal == true && other.GetComponent<PrTopDownCharInventory>().ActualHealth < other.GetComponent<PrTopDownCharInventory>().Health)
        {
            other.GetComponent<PrTopDownCharInventory>().ActualHealth += Health;
            if (other.GetComponent<PrTopDownCharInventory>().ActualHealth > other.GetComponent<PrTopDownCharInventory>().Health)
                other.GetComponent<PrTopDownCharInventory>().ActualHealth = other.GetComponent<PrTopDownCharInventory>().Health;
          //  other.GetComponent<PrTopDownCharInventory>().
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(false);
            transform.GetComponent<BoxCollider>().enabled = false;
            time = cooldown;
        }
    }


    //protected override void SetName()
    //{
    //    itemName = LoadType.ToString() + " Ammo";

    //}

    //public void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        itemName = LoadType.ToString() + " Ammo";

    //        if (Player != null)
    //        {
    //            PrTopDownCharInventory PlayerInv = Player.GetComponent<PrTopDownCharInventory>();

    //            if (PlayerInv.Weapon[ActiveWeapon].GetComponent<PrWeapon>() != null)
    //            {
    //                PlayerInv.Weapon[ActiveWeapon].GetComponent<PrWeapon>().LoadAmmo((int)LoadType);
    //            }


    //        }
    //    }
    //}



    //public void PickupObjectNow(int ActiveWeapon)
    //{

    //    if (Player != null)
    //    {
    //        PrTopDownCharInventory PlayerInv = Player.GetComponent<PrTopDownCharInventory>();

    //        if (PlayerInv.Weapon[ActiveWeapon].GetComponent<PrWeapon>() != null)
    //        {
    //            PlayerInv.Weapon[ActiveWeapon].GetComponent<PrWeapon>().LoadAmmo((int)LoadType);
    //        }


    //    }

    //    base.PickupObjectNow(ActiveWeapon);  
    //}
}
