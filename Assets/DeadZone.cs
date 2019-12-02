using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //Physics.gravity = new Vector3(0, -15, 0);

        if (other.CompareTag("Player"))
        {
          
        }
    }
}
