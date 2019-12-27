using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{ 
    [Header("Stats")]
    public int health;
    public int speed;

    [Header("Weapons")]
    public GameObject weapon;

    private Animator anim;
    private Rigidbody rb;
    private Vector2 smoothDeltaPosition = Vector2.zero;
    private Vector2 velocity = Vector2.zero;
    private Transform target;
    private NavMeshAgent agent;
    bool dead = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = transform.GetChild(0).GetComponent<Animator>();
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        //agent.updatePosition = false;
    }

    // Update is called once per frame
    void Update()
    {
        Run();
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

    private void OnAnimatorMove()
    {
        transform.position = agent.nextPosition;
    }

}
