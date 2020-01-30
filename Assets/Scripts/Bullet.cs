using BehaviorDesigner.Runtime.Tactical;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    // движение пули при включении из пула и коллижн с другими объектами
    public GameObject HitVFX;
    public GameObject HitDecal;
    public Vector3 hitpos;
    public bool sawblade;
    //public GameObject bulletPool;
    public GameObject VFXpool;

    [SerializeField]
    private Rigidbody rb;

    [HideInInspector]
    public WeaponController weaponController;

    public void OnEnable()
    {
        transform.position = weaponController.currentWeapon.shootPos.position;
        transform.rotation = weaponController.currentWeapon.shootPos.rotation;              

        // TODO: посмотреть зачем это
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(false);
        transform.GetChild(3).gameObject.SetActive(false);
        transform.GetChild(4).gameObject.SetActive(false);
        transform.GetComponent<MeshRenderer>().enabled = true;
    }

    void FixedUpdate()
    {
        GetComponent<Rigidbody>().MovePosition(transform.position + transform.forward * weaponController.currentWeapon.bulletForce * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        IDamageable damageable;
        if ((damageable = collision.gameObject.GetComponent(typeof(IDamageable)) as IDamageable) != null)
        {
            damageable.Damage(weaponController.currentWeapon.damage);
            transform.GetComponent<MeshRenderer>().enabled = false;
            transform.GetChild(1).gameObject.SetActive(true);
        }
        else if (collision.gameObject.CompareTag("Walls"))
        {
            transform.GetChild(2).gameObject.SetActive(true);
            transform.GetComponent<MeshRenderer>().enabled = false;
        }
        Disable();
    }

    private void BulletHit(Collider other, bool shouldRotate)
    {
        Quaternion rotation = Quaternion.identity;
        if (shouldRotate)
            rotation = Quaternion.LookRotation(transform.position, other.transform.position);
    }

    private void Disable()
    {
        gameObject.transform.SetParent(weaponController.currentWeapon.bulletPool.transform);
        gameObject.SetActive(false);
    }
}
