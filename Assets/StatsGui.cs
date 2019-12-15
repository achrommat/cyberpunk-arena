using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsGui : MonoBehaviour
{
    public GameObject Weapons;
    void FixedUpdate()
    {
        for(int i = 0; i < Weapons.transform.childCount; i++)
        {
            if(Weapons.transform.GetChild(i).gameObject.active)
            {
              transform.GetChild(0).GetComponent<Text>().text = ""+ (Weapons.transform.GetChild(i).GetComponent<Weapons>().Ammo);
            }
        }
    }
}
