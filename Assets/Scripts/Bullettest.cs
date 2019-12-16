using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullettest : MonoBehaviour
{
    public LayerMask collisionMask;
    public int damage;
    public float speed = 60;
    public GameObject HitVFX;
    public GameObject HitDecal;
    public int ricochet = 0;

     void FixedUpdate()
    {
       GetComponent<Rigidbody>().MovePosition(transform.position + transform.forward * speed * Time.fixedDeltaTime);
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit,Time.deltaTime * speed + .1f))
        {
            Vector3 reflectDir = Vector3.Reflect(ray.direction, hit.normal);
            float rot = 90 - Mathf.Atan2(reflectDir.z, reflectDir.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, rot, 0);
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            GameObject hit = Instantiate(HitVFX, transform.position, Quaternion.LookRotation(transform.position, other.transform.position));
            Destroy(gameObject);
            other.GetComponent<InteractableController>().health -= damage;
        }

        if (other.CompareTag("Walls"))
        {
           

          
            if (ricochet > 0)
            {
                Vector3 reflectDir = Vector3.Reflect(transform.position.normalized, other.transform.position.normalized);
                float rot = 90 - Mathf.Atan2(reflectDir.z, reflectDir.x) * Mathf.Rad2Deg;
                transform.eulerAngles = new Vector3(0, rot, 0);

            }
            else
            {
                //тут надо добить поворот попадения - Quaternion или забить хуй (пока второй вариант)
                //возможно надо делать через рейкаст (пример в PrBullet)
                GameObject hit = Instantiate(HitVFX, transform.position, Quaternion.LookRotation(transform.position, other.transform.position));
                Destroy(gameObject);
            }


        }
    }
}
