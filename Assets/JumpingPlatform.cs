using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingPlatform : MonoBehaviour
{
    public float force =100;
    public bool foward;
    private void OnTriggerEnter(Collider other)
    {
        //Physics.gravity = new Vector3(0, -15, 0);

        if (other.CompareTag("Player"))
        {
            if(foward == false)
            {
                other.GetComponent<Rigidbody>().AddForce(transform.up  * force);
            }
            if (foward == true)
            {
                //   other.GetComponent<Rigidbody>().AddForce(transform.up * force / 4);
                other.GetComponent<Rigidbody>().velocity = Vector3.zero;
                other.GetComponent<Rigidbody>().AddRelativeForce (Vector3.forward *force);
                other.attachedRigidbody.velocity = (transform.forward * force)*Time.deltaTime;

            }
        }
    }
}