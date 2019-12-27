using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingPlatform : MonoBehaviour
{
    public float force = 100;
    public bool foward;
    public bool UP;
    public GameObject target;


    public float speed = 1;
    public float AngleInDegrees;
    float g = Physics.gravity.y;
    public Transform SpawnTransform;


    private void OnTriggerEnter(Collider other)
    {
        //Physics.gravity = new Vector3(0, -15, 0);

        if (other.CompareTag("Player"))
        {
            //if (foward == false)
            //{
            //    other.GetComponent<Rigidbody>().AddForce(transform.up * force);
            //}
            //if (foward == true)
            //{
            //    //   other.GetComponent<Rigidbody>().AddForce(transform.up * force / 4);
            //  //  other.GetComponent<Rigidbody>().velocity = Vector3.zero;
            //   // other.GetComponent<Rigidbody>().AddForce(Vector3.forward * force);
            //  //  other.GetComponent<Rigidbody>().AddForce(transform.up * force);

            //    //  other.attachedRigidbody.velocity = (transform.forward * force) * Time.deltaTime;


            //    Vector3 jump = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z);
            //    other.GetComponent<Rigidbody>().AddForce(jump * force);
            //}

            if (!UP)
            {
                other.attachedRigidbody.velocity = (transform.forward * force) * Time.deltaTime;
                Vector3 fromTo = target.transform.position - transform.position;
                Vector3 fromToXZ = new Vector3(fromTo.x, 0f, fromTo.z);
                //  transform.rotation = Quaternion.LookRotation(fromToXZ, Vector3.up);
                float x = fromToXZ.magnitude;
                float y = fromTo.y;

                float AngleInRadians = AngleInDegrees * Mathf.PI / 180;

                float v2 = (g * x * x) / (2 * (y - Mathf.Tan(AngleInRadians) * x) * Mathf.Pow(Mathf.Cos(AngleInRadians), 2));
                float v = Mathf.Sqrt(Mathf.Abs(v2));

                // GameObject newBullet = Instantiate(Bullet, SpawnTransform.position, Quaternion.identity);
                //player.transform.SetParent(newBullet.transform);
                //newBullet.GetComponent<Rigidbody>().velocity = SpawnTransform[i].forward * v;
                //newBullet.GetComponent<Rigidbody>().velocity = SpawnTransform.forward * v;
                other.GetComponent<Rigidbody>().AddForce(SpawnTransform.forward * speed); //*v
                GetComponent<AudioSource>().Play();
            }
            else
            {
                other.GetComponent<Rigidbody>().AddForce(SpawnTransform.up * speed);
                GetComponent<AudioSource>().Play();
            }
          

        }
    }
    void PlayFootStepAudio()
    {

    }
}