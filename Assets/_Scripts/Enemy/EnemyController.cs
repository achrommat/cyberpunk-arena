using UnityEngine;
using UnityEngine.AI;
using BehaviorDesigner.Runtime.Tactical;
using BehaviorDesigner.Runtime;

public class EnemyController : BaseCharacterController
{
    [Header("VFX")]
    private Renderer myRenderer;
    [SerializeField] private Outline outline;
    private float outlineAlpha;
    [HideInInspector] public bool ShouldHighlight = false;

    public Vector3 target;
    public NavMeshAgent agent;

    public Collider myCollider;

    

    // Start is called before the first frame update
    protected override void Awake()
    {
        outlineAlpha = outline.outlineColor.a;
        myRenderer = FindRenderer();
        agent.updatePosition = false;
        stats.runSpeed = (agent.speed * 2);
    }

    public void ShowHighlightOnPlayerAiming()
    {
        outline.outlineColor.a = 1f;
        outline.needsUpdate = true;
    }

    public void HideHighlightOnPlayerAiming()
    {
        outline.outlineColor.a = outlineAlpha;
        outline.needsUpdate = true;
    }

    private Renderer FindRenderer()
    {
        Renderer rend = new Renderer();
        Renderer[] rends;
        rends = GetComponentsInChildren<Renderer>();
        foreach (Renderer localRenderer in rends)
        {
            if (localRenderer.gameObject.name.Contains("Character_") || localRenderer.gameObject.name.Contains("SM_Chr"))
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

    protected override void Update()
    {
        base.Update();
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude < 1.5f)
                {
                    run = 0;
                    aiming = true;
                    return;
                }
            }
        }
        aiming = false;
        run = 0.7f;
    }

    private void OnAnimatorMove()
    {
        transform.position = agent.nextPosition;
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
