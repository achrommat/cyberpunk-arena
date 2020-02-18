using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    // Переключение оружия у игрока и линк на текущее оружие
    public string currentWeaponName = "Assault_Rifle";
    
    public Weapon currentWeapon;
    public bool switchWeapon = true;
    [HideInInspector]
    public bool shooting;

    [SerializeField] private List<Weapon> weaponList;    
    private Dictionary<string, Weapon> weapons;
    [SerializeField] private GameObject weaponFolder;

    [SerializeField] private PlayerController player;

    private void Awake()
    {
        // Добавляем все оружия на игроке в лист
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

        if (player)
        {
            if (!player.aiming && currentWeapon.devotion)
            {
                currentWeapon.ResetDevotion();
            }
        }        
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
        currentWeapon.gameObject.SetActive(true);
    }

    private void FindWeapon()
    {
        // Находим нужное оружие по названию
        var weaponToSwitch = weapons.FirstOrDefault(t => t.Key == currentWeaponName);
        currentWeapon = weaponToSwitch.Value;
        currentWeaponName = weaponToSwitch.Key;              
    }
}
