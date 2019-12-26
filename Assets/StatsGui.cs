using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsGui : MonoBehaviour
{
    private GameObject Weapons;
    public GameObject Player;
    private void Awake()
    {
        Weapons = Player.GetComponent<CharController>().Weapons.gameObject;
    }
    void FixedUpdate()
    {
        for(int i = 0; i < Weapons.transform.childCount; i++)
        {
            
            transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "" + Player.GetComponent<CharController>().Health;
            transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "" + Player.GetComponent<CharController>().Armor;
            if (Weapons.transform.GetChild(i).gameObject.active)
            {
                transform.GetChild(2).GetChild(0).GetComponent<Text>().text = "" + (Weapons.transform.GetChild(i).GetComponent<Weapons>().Ammo);
            }
        }
    }
}
