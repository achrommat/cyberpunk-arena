using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ExplosiveBotController : MonoBehaviour
{
    public GameObject explosive;
    public bool canRun;
    public int health;
    private Animator anim;
    private Rigidbody rb;
    private Collider col;
    private Vector2 smoothDeltaPosition = Vector2.zero;
    private Vector2 velocity = Vector2.zero;
    private Transform target;
    private NavMeshAgent agent;


    public float maxSize;
    public float growFactor;
    public float waitTime;
    public GameObject fillRadius;

    IEnumerator FillRadius()
    {
        float timer = 0;
        while (true)
        {
            while (maxSize > fillRadius.transform.localScale.x)
            {
                timer += Time.deltaTime;
                fillRadius.transform.localScale += new Vector3(0.1f, 0.1f, 0) * Time.deltaTime * growFactor;
                yield return null;
            }
            yield return new WaitForSeconds(waitTime);
        }
    }

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
        if (health < 1)
            RunExplosion();
    }

    private void FixedUpdate()
    {
        if (velocity.magnitude > 0.5f && agent.remainingDistance <= 3f)
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
        fillRadius.transform.parent.gameObject.SetActive(true);
        StartCoroutine(FillRadius());
        //col.enabled = false;
        //rb.AddForce(new Vector3(0, 2, 0), ForceMode.Impulse);
        //anim.SetBool("Jump", true);
    }

    private void OnAnimatorMove()
    {
        transform.position = agent.nextPosition;
    }

    public void RunExplosion()
    {
        GameObject explosion = Instantiate(explosive, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        Destroy(gameObject);
    }
}
