using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public int ActiveWeapon =1;
    public bool shooting;

    void Update()
    {
        transform.GetChild(ActiveWeapon).GetComponent<Weapons>().shooting = shooting;
        if(Input.GetKeyUp(KeyCode.Q) && ActiveWeapon > 1)
        {
            PreviousVeapon();
        }
        if (Input.GetKeyUp(KeyCode.E) && ActiveWeapon < transform.childCount -1)
        {
            NextVeapon();
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
    public void NextVeapon()
    {
        ActiveWeapon++;
        WeaponSelect();
    }
    public void PreviousVeapon()
    {
        ActiveWeapon--;
        WeaponSelect();
    }
}
