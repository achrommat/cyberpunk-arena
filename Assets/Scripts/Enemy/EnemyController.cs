using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : BaseCharacterController
{     
    [Header("VFX")]
    public bool flashWhenHit = true;
    private Renderer myRenderer;    
    public Transform target;
    public NavMeshAgent agent;
    public bool dead = false;
    
    // Start is called before the first frame update
    void Start()
    {
        myRenderer = FindRenderer();
    }

    private Renderer FindRenderer()
    {
        Renderer rend = new Renderer();
        Renderer[] rends;
        rends = GetComponentsInChildren<Renderer>();
        foreach (Renderer localRenderer in rends)
        {
            if (localRenderer.gameObject.name.Contains("Character_"))
            {
                rend = localRenderer;
                break;
            }
            else
            {
                continue;
            }
        }
        return rend;
    }    

    private void OnAnimatorMove()
    {
        transform.position = agent.nextPosition;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullets"))
        {
            if (stats.IsAlive())
            {
                GetDamage();
            }                
        }            
    }

    public void GetDamage()
    {
        if (flashWhenHit)
        {
            StartCoroutine(Flash());
        }            
    }

    IEnumerator Flash()
    {
        myRenderer.material.color = Color.red;
        yield return new WaitForSeconds(.1f);
        myRenderer.material.color = Color.white;
    }

    protected override void Respawn()
    {
        actualRespawnTime -= Time.deltaTime;
        if (actualRespawnTime <= 0 && respawnTarget != null)
        {
            actualRespawnTime = respawnTime;
            stats.currentHealth = stats.health;
            transform.position = respawnTarget;
            Instantiate(RespawnVFX, transform.position, transform.rotation);
        }
    }

}
