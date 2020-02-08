using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableController : MonoBehaviour
{
    public int health;
    public GameObject explosive;
    public GameObject trail;
    public GameObject explosionDamageZone;
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
        GameObject explosionZone = Instantiate(explosionDamageZone, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        explosion.transform.localScale = new Vector3(3f, 3f, 3f);        
        Destroy(explosion, 3f);
        Destroy(explosionZone, 0.2f);
        if (gameObject.name == "SM_Veh_Classic_0")
        {
            explosionZone.transform.localScale = new Vector3(4f, 4f, 4f);
            meshRenderers = gameObject.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer meshRenderer in meshRenderers)
                meshRenderer.material = blackMat;
            return;
        }
        explosionZone.transform.localScale = new Vector3(3f, 3f, 3f);
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
