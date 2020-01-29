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
        hit = false;
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(false);
        transform.GetChild(3).gameObject.SetActive(false);
        transform.GetChild(4).gameObject.SetActive(false);

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
        }
        else 
        {
            gameObject.transform.SetParent(BulletPool.transform);
            gameObject.SetActive(false);
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            hit = true;
            other.GetComponent<CharController>().Health -= damage;
            //gameObject.transform.parent = other.transform.GetChild(0).transform;
            //  Destroy(gameObject);
            if (lifetimer < 0)
            {
                gameObject.transform.SetParent(BulletPool.transform);
                gameObject.SetActive(false);
            }
        }
        if (other.CompareTag("Club"))
        {
            other.transform.GetComponent<RandomDance>().HP -= damage;
            transform.GetChild(1).gameObject.SetActive(true);
            // transform.GetChild(0).gameObject.SetActive(false);
            hit = true;
            //  Destroy(gameObject);
        }

        if (other.CompareTag("Player"))
        {
            hit = true;
            other.transform.GetChild(0).GetComponent<CharController>().Health -= damage;
            gameObject.transform.parent = other.transform.GetChild(0).GetChild(1).transform;

            //  Destroy(gameObject);
     
        }
        if (other.CompareTag("Walls"))
        {
            transform.GetChild(2).gameObject.SetActive(true);
            hit = true;
            // gameObject.transform.parent = other.transform;
            //  Destroy(gameObject);

        }

    }
}
