using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    public int Damage = 1000;
    public bool friendly = false;
 
    private void OnTriggerEnter(Collider other)
    {
        //Physics.gravity = new Vector3(0, -15, 0);

        if (other.CompareTag("Player") && !friendly)
        {
            other.transform.GetChild(0).transform.GetComponent<CharController>().Health -= Damage;
        }
        if (other.CompareTag("Enemy") && friendly)
        {
            other.transform.GetComponent<BaseEnemyController>().currentHealth -= Damage;
        }

    }
}
