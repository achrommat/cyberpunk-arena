using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoverbike_Interactable : MonoBehaviour
{
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
}
