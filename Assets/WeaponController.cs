using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public int ActiveWeapon =1;
    public bool shooting;


    void Update()
    {
        for(int i = 0; i< transform.childCount; i++)
        {
            if(transform.GetChild(i).gameObject.active)
            {
                transform.GetChild(i).GetComponent<Weapons>().shooting = shooting;
            }
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            PreviousVeapon();
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            NextVeapon();
        }
    }

    public void NextVeapon()
    {
        if (ActiveWeapon < transform.childCount - 1)
        {
            ActiveWeapon++;
            WeaponSelect();
        }
    }
    public void PreviousVeapon()
    {
        if(ActiveWeapon > 1)
        {
            ActiveWeapon--;
            WeaponSelect();
        }
    }
    public void WeaponSelect()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        transform.GetChild(ActiveWeapon).gameObject.SetActive(true);
    }
}
