using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullettest : MonoBehaviour
{
    float speed = 60;
    public GameObject HitVFX;
    public GameObject HitDecal;

    void Update()
    {
       GetComponent<Rigidbody>().MovePosition(transform.position + transform.forward * speed * Time.fixedDeltaTime);
    }
    public void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag("Player"))
        {
            Debug.Log("Рома хуй");
        }
        if (other.CompareTag("Walls"))
        {
            GameObject hit = Instantiate(HitVFX, transform.position, transform.rotation);
        //    Destroy(hit, 1);
            //GameObject decal = Instantiate(HitDecal, transform.position, transform.rotation);
            //Destroy(decal, 5);
            Destroy(gameObject);
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        //чет нихуя не работает тут
        Debug.Log("Работай сука");
        GameObject hit = Instantiate(HitVFX, transform.position, transform.rotation);
        Destroy(hit, 1);
        GameObject decal = Instantiate(HitDecal, transform.position, transform.rotation);
        Destroy(decal, 5);
        Destroy(gameObject);
    }
}
