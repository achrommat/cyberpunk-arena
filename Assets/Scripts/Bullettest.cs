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
        if (other.CompareTag("Walls"))
        {
            //тут надо добить поворот попадения - Quaternion или забить хуй (пока второй вариант)
            //возможно надо делать через рейкаст (пример в PrBullet)
            GameObject hit = Instantiate(HitVFX, transform.position, Quaternion.LookRotation(transform.position, other.transform.position));
            Destroy(gameObject);
        }
    }
}
