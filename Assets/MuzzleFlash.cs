using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlash : MonoBehaviour
{
    public WeaponController weaponController;

    // Update is called once per frame
    void Update()
    {
        transform.position = weaponController.currentWeapon.muzzleFlashPos.position;
        StartCoroutine(Despawn());
    }

    private IEnumerator Despawn()
    {
        yield return new WaitForSeconds(0.5f);
        MF_AutoPool.Despawn(gameObject, 2f);
    }
}
