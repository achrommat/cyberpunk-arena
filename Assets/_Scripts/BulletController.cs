using BehaviorDesigner.Runtime.Tactical;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletController : MonoBehaviour
{
    // движение пули при включении из пула и коллижн с другими объектами
    [HideInInspector] public bool sawblade;
    [HideInInspector] public bool isEnemy = false;
    //public GameObject bulletPool;
    public GameObject VFXpool;
    [HideInInspector] public float damage = 0.5f;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private AP_Reference poolRef;

    [HideInInspector] public WeaponController weaponController;

    [SerializeField] private GameObject bulletImpact;

    void FixedUpdate()
    {
        rb.MovePosition(transform.position + transform.forward * weaponController.currentWeapon.bulletForce * Time.fixedDeltaTime);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            return;
        }

        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;
        GameObject newbulletImpact = MF_AutoPool.Spawn(bulletImpact, pos, rot);

        IDamageable damageable;
        if ((damageable = collision.gameObject.GetComponent(typeof(IDamageable)) as IDamageable) != null)
        {
            // если враг, то урон забираем с AIShootable. Если игрок, то с оружия
            if (isEnemy && !collision.gameObject.CompareTag("Enemy"))
            {                
                damageable.Damage(damage);
            }
            else
            {
                damageable.Damage(weaponController.currentWeapon.damage);
            }            
        }
        MF_AutoPool.Despawn(poolRef, 5f);
    }

    private void BulletHit(Collider other, bool shouldRotate)
    {
        Quaternion rotation = Quaternion.identity;
        if (shouldRotate)
            rotation = Quaternion.LookRotation(transform.position, other.transform.position);
    }
}
