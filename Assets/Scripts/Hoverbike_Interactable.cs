using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoverbike_Interactable : MonoBehaviour
{
    public int health;
    public GameObject explosive;
    public GameObject trail;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void RunTrail()
    {
        trail.GetComponent<ParticleSystem>().Play();
    }

    public void RunExplosion()
    {
        Instantiate(explosive, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        Destroy(gameObject);
    }

    private void Update()
    {
        if (health < 1 && !anim.GetBool("Run_Animation"))
        {
            Activate();
        }
    }

    private void Activate()
    {
        anim.SetBool("Run_Animation", true);
    }
}
