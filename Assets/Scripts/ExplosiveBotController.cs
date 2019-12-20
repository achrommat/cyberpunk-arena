using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ExplosiveBotController : MonoBehaviour
{
    public GameObject explosive;
    public bool canRun;
    public int health;
    public float maxSize;
    public float growFactor;
    public float waitTime;
    public GameObject fillRadius;
    public GameObject explosionCharge;

    private Animator anim;
    private Rigidbody rb;
    private Collider col;
    private Vector2 smoothDeltaPosition = Vector2.zero;
    private Vector2 velocity = Vector2.zero;
    private Transform target;
    private NavMeshAgent agent;
    bool dead = false;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        col = GetComponent<Collider>();
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        agent.updatePosition = false;
    }

    private void Update()
    {        
        if (canRun)
            Run();        
    }

    private void FixedUpdate()
    {
        if (health < 1)
            RunExplosion();
        if (velocity.magnitude > 0.5f && agent.remainingDistance <= 2f)
            Explode();
    }

    private void Run()
    {
        agent.SetDestination(target.position);
        Vector3 worldDeltaPosition = agent.nextPosition - transform.position;

        float dx = Vector3.Dot(transform.right, worldDeltaPosition);
        float dy = Vector3.Dot(transform.forward, worldDeltaPosition);
        Vector3 deltaPosition = new Vector2(dx, dy);

        float smooth = Mathf.Min(1f, Time.deltaTime / 0.15f);
        smoothDeltaPosition = Vector2.Lerp(smoothDeltaPosition, deltaPosition, smooth);

        if (Time.deltaTime > 1e-5f)
        {
            velocity = smoothDeltaPosition / Time.deltaTime;
        }

        bool shouldMove = velocity.magnitude > 0.5f && agent.remainingDistance > agent.radius;

        anim.SetBool("Move", shouldMove);        
    }

    private void Explode()
    {
        agent.isStopped = true;
        explosionCharge.SetActive(true);
        StartCoroutine(ScaleCharge());
        fillRadius.transform.parent.gameObject.SetActive(true);       
        StartCoroutine(FillRadius());
        //col.enabled = false;
        //rb.AddForce(new Vector3(0, 2, 0), ForceMode.Impulse);
        //anim.SetBool("Jump", true);
    }

    IEnumerator ScaleCharge()
    {
        float timer = 0;
        float maxScale = 5;
        while (true)
        {
            while (maxScale > explosionCharge.transform.localScale.x)
            {
                timer += Time.fixedDeltaTime;
                explosionCharge.transform.localScale += new Vector3(0.1f, 0.1f, 0) * Time.fixedDeltaTime * growFactor;
                yield return null;
            }
            yield return new WaitForSeconds(waitTime);
        }
    }

    IEnumerator FillRadius()
    {
        //float timer = 0;
        while (true)
        {
            while (maxSize > fillRadius.transform.localScale.x)
            {
                //timer += Time.fixedDeltaTime;
                fillRadius.transform.localScale += new Vector3(0.1f, 0.1f, 0f) * Time.fixedDeltaTime * growFactor;
                yield return null;
            }
            yield return new WaitForSeconds(waitTime);
            RunExplosion();
        }
    }

    private void OnAnimatorMove()
    {
        transform.position = agent.nextPosition;
    }

    public void RunExplosion()
    {
        if (dead == false)
        {
            GameObject explosion = Instantiate(explosive, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.Euler(-90f, 0f, 0f));
            explosion.GetComponent<SphereCollider>().radius = 2;            
            Destroy(gameObject);
            Destroy(explosion, 0.5f);
            dead = true;
        } 
    }
}
