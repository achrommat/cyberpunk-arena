using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStuff : MonoBehaviour
{
    public int riflebullets = 0;
    public int laserbullets = 0;
    public int rocketbullets = 0;
    public int pistolbullets = 0;
    public int shotgunbullets = 0;
    public int ActiveWeapon;
    public GameObject weapons;

    public void Update()
    {
        //weapons.transform.GetChild(1).GetComponent<PrWeapon>().Bullets = bullets;
    }
}
