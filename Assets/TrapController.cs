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
    float movetimer;
    float delaytimer;
    bool foward = false;
    void Start()
    {
        RB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(foward)
        {
            movetimer -= Time.fixedDeltaTime;
            RB.velocity = Axis * speed;
            if(movetimer <= 0)
            {
                foward = false;
            }
        }
        else
        {
            movetimer += Time.fixedDeltaTime;
            RB.velocity = -Axis * speed;
            if(movetimer>=timer)
            {
                foward = true;
            }
        }
    }
}
