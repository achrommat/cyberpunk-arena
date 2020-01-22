using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    Rigidbody RB;
    public float timer;
    public float speed;
    public float delay;
    public GameObject Target;
    public Vector3 Axis;
    float movetimer =0;
    float delaytimer;
    bool foward = false;
    void Start()
    {
        movetimer = timer;
        RB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(foward)
        {
            movetimer -= Time.deltaTime;
            transform.position = transform.position + Axis * Time.deltaTime * speed;
            if(movetimer <= 0)
            {
                foward = false;
            }
        }
        else
        {
            movetimer += Time.deltaTime;
            transform.position = transform.position - Axis * Time.deltaTime * speed;
            if (movetimer>=timer)
            {
                foward = true;
            }
        }
    }
}
