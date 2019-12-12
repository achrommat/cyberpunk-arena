using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CannonController : MonoBehaviour
{

    public Transform SpawnTransform;
    public Transform TargetTransform;
    GameObject player;

    public GameObject Bullet;

    //public GameObject Bullet;

   // public GameObject CannonFX;

    public float speed = 1;


    public float AngleInDegrees;

    float g = Physics.gravity.y;

    private void Awake()
    {
        //for (int i = 0; i <=)
        //    SpawnTransform.Length = new transform.childCount;
        
    }
    void Update()
    {  
        //    SpawnTransform.localEulerAngles = new Vector3(-AngleInDegrees, 0f, 0f); 

        //if (Input.GetMouseButtonDown(0))
        //{
        //    StartCoroutine(Shoot());
        //}
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")|| other.CompareTag("Test"))
        {
            player = other.gameObject;
            Jump();
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Test"))
        {
            player = null;
        }
    }


    public void Jump()
    {
        Vector3 fromTo = TargetTransform.position - transform.position;
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
        player.GetComponent<Rigidbody>().AddForce(SpawnTransform.forward * v * speed);

    }


}
