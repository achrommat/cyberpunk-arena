using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public string currentWeaponName = "Rifle";
    private Weapon currentWeapon;
    public bool switchWeapon = true;
    public bool shooting;

    [SerializeField]
    private List<Weapon> weaponList;    
    private Dictionary<string, Weapon> weapons;
    [SerializeField]
    private GameObject weaponFolder;    

    private void Awake()
    {
        weapons = new Dictionary<string, Weapon>();
        weaponList.AddRange(weaponFolder.GetComponentsInChildren<Weapon>(true));
        foreach (Weapon gun in weaponList)
        {
            weapons.Add(gun.name, gun);
        }
        FindWeapon();
    }

    void Update()
    {
        SwitchWeapon();
        currentWeapon.shooting = this.shooting;
    }

    public void SwitchWeapon()
    {
        if (!switchWeapon)
        {
            return;
        }
        switchWeapon = false;
        foreach (Weapon weapon in weaponList)
        {
            weapon.gameObject.SetActive(false);
        }
        FindWeapon();
    }

    private void FindWeapon()
    {
        var weaponToSwitch = weapons.FirstOrDefault(t => t.Key == currentWeaponName);
        currentWeapon = weaponToSwitch.Value;
        currentWeapon.gameObject.SetActive(true);
        currentWeaponName = weaponToSwitch.Key;
    }
}
