using BehaviorDesigner.Runtime.Tactical;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletController : MonoBehaviour
{
    // движение пули при включении из пула и коллижн с другими объектами
    public GameObject HitVFX;
    public GameObject HitDecal;
    public Vector3 hitpos;
    public bool sawblade;
    //public GameObject bulletPool;
    public GameObject VFXpool;
    public TrailRenderer trail;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private AP_Reference poolRef;

    [HideInInspector]
    public WeaponController weaponController;

    /*public void OnEnable()
    {
        transform.position = weaponController.currentWeapon.shootPos.position;
        transform.rotation = weaponController.currentWeapon.shootPos.rotation;    
    }*/

    void FixedUpdate()
    {
        rb.MovePosition(transform.position + transform.forward * weaponController.currentWeapon.bulletForce * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        IDamageable damageable;
        if ((damageable = collision.gameObject.GetComponent(typeof(IDamageable)) as IDamageable) != null)
        {
            damageable.Damage(weaponController.currentWeapon.damage);
            //transform.GetComponent<MeshRenderer>().enabled = false;
            //transform.GetChild(1).gameObject.SetActive(true);
        }
        else if (collision.gameObject.CompareTag("Walls"))
        {
            //transform.GetChild(2).gameObject.SetActive(true);
            //transform.GetComponent<MeshRenderer>().enabled = false;
        }
        MF_AutoPool.Despawn(gameObject, 1f);

        //Disable();
    }

    private void BulletHit(Collider other, bool shouldRotate)
    {
        Quaternion rotation = Quaternion.identity;
        if (shouldRotate)
            rotation = Quaternion.LookRotation(transform.position, other.transform.position);
    }

    /*private void Disable()
    {
        gameObject.transform.SetParent(weaponController.currentWeapon.bulletPool.transform);
        gameObject.SetActive(false);
    }*/
}
