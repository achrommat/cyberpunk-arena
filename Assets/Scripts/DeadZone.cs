using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    public float Damage = 1000;
    private void OnTriggerEnter(Collider other)
    {
        //Physics.gravity = new Vector3(0, -15, 0);

        if (other.CompareTag("Player"))
        {
            other.transform.GetChild(0).transform.GetComponent<CharController>().Health -= Damage;
        }
    }
}
