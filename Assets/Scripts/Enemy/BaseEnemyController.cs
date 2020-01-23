using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemyController : MonoBehaviour
{ 
    [Header("Stats")]
    public int maxHealth;
    public float damage;
    public float respawnTime;
    public float respawnTimer;
    public int currentHealth;

    [Header("Weapons")]
    public bool multishot;
    public AudioClip shotClip;
    public AudioSource audioSource;
    public GameObject shootPos;
    public float recoil = 0;
    public GameObject bulletPool;
    public GameObject weapon;
    //public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask playerLayer;
    public float attackRate = 1f;
    public float nextAttackTime = 0f;

    public GameObject respawnTarget;

    [Header("VFX")]
    public GameObject respawnVFX;
    public bool flashWhenHit = true;
    private Renderer myRenderer;
    //public GameObject deadVFX;

    public Animator anim;
    private Rigidbody rb;
    private Vector2 smoothDeltaPosition = Vector2.zero;
    private Vector2 velocity = Vector2.zero;
    public Transform target;
    public NavMeshAgent agent;
    public bool dead = false;

    private float run = 1f;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = attackRange;
        myRenderer = FindRenderer();
        //agent.updatePosition = false;
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

    // Update is called once per frame
    protected virtual void Update()
    {
        if (!dead)
            Run();
        DeathHandler();
        anim.SetBool("Dead", dead);
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    protected virtual IEnumerator Attack()
    {
        // реализуется в наследуемых скриптах
        return null;
    }

    protected void CreateBullet()
    {
        GameObject newbullet = bulletPool.transform.GetChild(0).gameObject;
        newbullet.GetComponent<BaseEnemyBulletController>().damage = this.damage;
        newbullet.transform.position = shootPos.transform.position;
        newbullet.transform.rotation = shootPos.transform.rotation;
        newbullet.transform.SetParent(null);
        newbullet.SetActive(true);
    }

    protected virtual void Run()
    {
        agent.SetDestination(target.position);

        float distance = Vector3.Distance(target.position, transform.position);

        FaceTarget();

        if (distance <= attackRange)
        {
            if (Time.time >= nextAttackTime)
            {
                StartCoroutine(Attack());
                nextAttackTime = Time.time + attackRate;
            }
        }
        
        //Vector3 worldDeltaPosition = agent.nextPosition - transform.position;

        //float dx = Vector3.Dot(transform.right, worldDeltaPosition);
        //float dy = Vector3.Dot(transform.forward, worldDeltaPosition);
        //Vector3 deltaPosition = new Vector2(dx, dy);

        //float smooth = Mathf.Min(1f, Time.deltaTime / 0.15f);
        //smoothDeltaPosition = Vector2.Lerp(smoothDeltaPosition, deltaPosition, smooth);

        //if (Time.deltaTime > 1e-5f)
        //{
        //    velocity = smoothDeltaPosition / Time.deltaTime;
        //}

        //bool shouldMove = velocity.magnitude > 0.5f && agent.remainingDistance > agent.radius;

        //anim.SetBool("Move", shouldMove);
    }

    private void OnAnimatorMove()
    {
        transform.position = agent.nextPosition;
    }

    //private void UpdateAnimator()
    //{
    //    anim.SetFloat("Run", run);
    //    anim.SetFloat("Speed", agent.speed);
    //}

    protected void DeathHandler()
    {
        if (currentHealth < 1)
        {
            agent.isStopped = true;
            dead = true;           
            Respawn();
        }
    }

    /*void DeathExtra()
    {
        if (health <= -50 && DeadExtra == false)
        {
            Instantiate(DeadVFX, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), transform.rotation);
            Weapons.transform.GetComponent<WeaponController>().shooting = false;
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            Weapons.SetActive(false);
            rb.velocity = Vector3.zero;
            DeadExtra = true;
            Dead = true;
        }
    }*/

    protected virtual void Respawn()
    {
        respawnTimer += Time.deltaTime;
        if (respawnTimer > respawnTime)
        {
            dead = false;
            respawnTimer = 0.0f;
            Instantiate(respawnVFX, transform.position, transform.rotation);
            currentHealth = maxHealth;
            Vector3 resp = respawnTarget.transform.GetChild(Random.Range(0, respawnTarget.transform.childCount)).transform.position;
            transform.position = resp;
            agent.isStopped = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullets"))
        {
            if (!dead)
                GetDamage();
        }            
    }

    public void GetDamage()
    {
        // TODO: перенести сюда код из BulletTest
        // или это вынести в получение урона врагов в BulletTest
        if (flashWhenHit)
            StartCoroutine(Flash());
    }

    IEnumerator Flash()
    {
        myRenderer.material.color = Color.red;
        yield return new WaitForSeconds(.1f);
        myRenderer.material.color = Color.white;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

}
