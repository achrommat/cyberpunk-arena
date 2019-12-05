using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullettest : MonoBehaviour
{
    float speed =40;
    void Update()
    {
       GetComponent<Rigidbody>().MovePosition(transform.position + transform.forward * speed * Time.fixedDeltaTime);
    }
}
