using BehaviorDesigner.Runtime.Tactical;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    // TODO - переписать нахуй все здесь
    public LayerMask collisionMask;
    public float damage;
    public float speed = 60;
    public GameObject HitVFX;
    public GameObject HitDecal;
    public float lifetime = 1;
    public int through = 0;
    int currentThrough = 0;
    float lifetimer;
    public int ricochet = 0;
    public Vector3 hitpos;
    public bool sawblade;
    public GameObject BulletPool;
    public GameObject VFXpool;
    bool hit = false;
    public float hittime = 1;


    public void OnEnable()
    {
        hittime = 0.5f;
        currentThrough = through;
        lifetimer = lifetime;
        hit = false;
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(false);
        transform.GetChild(3).gameObject.SetActive(false);
        transform.GetChild(4).gameObject.SetActive(false);

        transform.GetComponent<MeshRenderer>().enabled = true;
    }

    void FixedUpdate()
    {
        if (hit)
        {
            hittime -= Time.deltaTime;
            if (hittime <= 0)
            {
                Disable();
            }
        }

        lifetimer -= Time.fixedDeltaTime;
        if (lifetimer <= 0 && !hit)
        {
            Disable();
        }

        if (lifetimer > 0 && hit == false)
        {
            GetComponent<Rigidbody>().MovePosition(transform.position + transform.forward * speed * Time.fixedDeltaTime);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        IDamageable damageable;
        if ((damageable = other.gameObject.GetComponent(typeof(IDamageable)) as IDamageable) != null && !other.CompareTag("Player"))
        {
            damageable.Damage(damage);
            transform.GetComponent<MeshRenderer>().enabled = false;
            transform.GetChild(1).gameObject.SetActive(true);
            hit = true;
        }
        else if (other.CompareTag("Walls"))
        {
            transform.GetChild(2).gameObject.SetActive(true);
            transform.GetComponent<MeshRenderer>().enabled = false;
            hit = true;
        }
    }

    private void BulletHit(Collider other, bool shouldRotate)
    {
        Quaternion rotation = Quaternion.identity;
        if (shouldRotate)
            rotation = Quaternion.LookRotation(transform.position, other.transform.position);

        //  GameObject hit = Instantiate(HitVFX, transform.position, rotation);
        //  Destroy(gameObject);
        if (lifetime < 0)
        {
            gameObject.transform.SetParent(BulletPool.transform);
            gameObject.SetActive(false);
        }
    }

    private void Disable()
    {
        gameObject.transform.SetParent(BulletPool.transform);
        gameObject.SetActive(false);
    }
}
