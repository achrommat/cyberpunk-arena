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
    private Component[] meshRenderers;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void RunTrail()
    {
        //trail.GetComponent<ParticleSystem>().Play();
        trail.gameObject.SetActive(true);
    }

    public void RunExplosion()
    {
        GameObject explosion = Instantiate(explosive, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        explosion.transform.localScale = new Vector3(3f, 3f, 3f);
        if (gameObject.name == "SM_Veh_Classic_0")
        {
            meshRenderers = gameObject.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer meshRenderer in meshRenderers)
                meshRenderer.material = blackMat;
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
