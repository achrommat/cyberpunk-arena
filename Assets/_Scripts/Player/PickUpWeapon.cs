using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpWeapon : MonoBehaviour
{
    public new string name;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            WeaponController weaponController = other.GetComponent<WeaponController>();
            weaponController.currentWeaponName = name;
            weaponController.switchWeapon = true;
            //gameObject.SetActive(false);
        }
    }
}
