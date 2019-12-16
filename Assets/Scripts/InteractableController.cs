using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableController : MonoBehaviour
{
    public int health;
    public GameObject explosive;
    public GameObject trail;
    public Material blackMat;
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
        Instantiate(explosive, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
        if (gameObject.name == "SM_Veh_Classic_0")
        {
            gameObject.GetComponentInChildren<MeshRenderer>().material = blackMat;
            return;
        }
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
