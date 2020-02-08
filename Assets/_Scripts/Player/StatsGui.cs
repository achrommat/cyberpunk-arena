using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsGui : MonoBehaviour
{
    private GameObject weapons;
    public GameObject Player;
    /*private void Awake()
    {
        weapons = Player.GetComponent<PlayerController>().Weapons.gameObject;
    }
    void FixedUpdate()
    {
        for (int i = 0; i < weapons.transform.childCount; i++)
        {

            //transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "" + Player.GetComponent<CharController>().Health;
            //transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "" + Player.GetComponent<CharController>().Armor;
            if (weapons.transform.GetChild(i).gameObject.active)
            {
                transform.GetChild(2).GetChild(0).GetComponent<Text>().text = "" + (weapons.transform.GetChild(i).GetComponent<Weapon>().ammo);
            }
        }
    }*/
}
