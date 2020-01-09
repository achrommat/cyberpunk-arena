using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipaController : MonoBehaviour
{
    public LayerMask collisionMask;
    public int damage;
    public float speed = 60;
    public GameObject HitVFX;
    public GameObject HitDecal;
    public float lifetime = 1;
    float lifetimer;
    public int ricochet = 0;
    public Vector3 hitpos;
    public bool sawblade;
    public GameObject BulletPool;
    public GameObject VFXpool;
    public bool hit;

    public void OnEnable()
    {
        lifetimer = lifetime;
    }
    void FixedUpdate()
    {
        lifetimer -= Time.fixedDeltaTime;
        if (lifetimer <= 0)
        {
            // Destroy(gameObject);
            gameObject.transform.SetParent(BulletPool.transform);
            gameObject.SetActive(false);
        }

        if (lifetimer > 0 && hit == false)
        {
            GetComponent<Rigidbody>().MovePosition(transform.position + transform.forward * speed * Time.fixedDeltaTime);
            if (ricochet > 0)
            {
                Ray ray = new Ray(transform.position, transform.forward);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, Time.deltaTime * speed + .1f))
                {
                    Vector3 reflectDir = Vector3.Reflect(ray.direction, hit.normal);
                    float rot = 90 - Mathf.Atan2(reflectDir.z, reflectDir.x) * Mathf.Rad2Deg;
                    transform.eulerAngles = new Vector3(0, rot, 0);
                    // hitpos = hit.point;
                }
            }
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            hit = true;
            other.transform.GetChild(0).GetComponent<CharController>().Health -= damage;
            gameObject.transform.parent = other.transform.GetChild(0).GetChild(0).transform;
            //  Destroy(gameObject);
            if(lifetime <0)
            {
                gameObject.transform.SetParent(BulletPool.transform);
                gameObject.SetActive(false);
            }
        }
        if (other.CompareTag("Walls"))
        {
            hit = true;
            gameObject.transform.parent = other.transform;
            //  Destroy(gameObject);
            if (lifetime < 0)
            {
                gameObject.transform.SetParent(BulletPool.transform);
                gameObject.SetActive(false);
            }
        }

    }
}
