using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetAim : MonoBehaviour
{
    bool HaveTarget;
    public List<Transform> target = new List<Transform>();
    public GameObject body;
    private Quaternion bodyrot;

    private void Awake()
    {
        bodyrot = body.transform.localRotation;
    }
    private void Update()
    {
        if(HaveTarget == true)
        {
            transform.GetChild(0).LookAt(target[0]);
            body.transform.LookAt(target[0]);
        }
        else
        {
            transform.GetChild(0).localRotation = Quaternion.Euler(0, 0, 0);
            body.transform.localRotation = bodyrot;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            target.Add(other.transform);
            HaveTarget = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            target.Remove(other.transform);
            if (target.Count == 0)
            {
                HaveTarget = false;
            }
        }
    }
}
