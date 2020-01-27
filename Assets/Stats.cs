using BehaviorDesigner.Runtime.Tactical;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour, IDamageable
{
    [Header("Main Stats")]
    public float Health;
    public float healthCount;

    public float runSpeed;
    public float startRunSpeed;
    public float speedWithAim;
    public float runSpeedMody;
    //public float speedaim;
    public float speedsetting = 1;
    public void Awake()
    {
        healthCount = Health;
    }

    void IDamageable.Damage(float amount)
    {
        Health--;
        //throw new System.NotImplementedException();
    }

    bool IDamageable.IsAlive()
    {
        return Health > 0;
        //throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        runSpeed = (startRunSpeed + speedWithAim + runSpeedMody);
    }
}
