using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject target;
    public GameObject FX;
    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.transform.position = target.transform.position;
            if(FX !=null)
            {
                GameObject FX1 = Instantiate(FX, transform.position, Quaternion.identity);
                GameObject FX2 = Instantiate(FX, target.transform.position, Quaternion.identity);
                Destroy(FX1, 1.5f);
                Destroy(FX2, 1.5f);
            }
          
           
        }
    }
}
