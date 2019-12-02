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
            GameObject FX1 = Instantiate(FX, transform.position, Quaternion.identity);
            GameObject FX2 = Instantiate(FX, target.transform.position, Quaternion.identity);
            Destroy(FX1, 0.6f);
            Destroy(FX2, 0.6f);
        }
    }
}
