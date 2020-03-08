using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System.Collections;
using BehaviorDesigner.Runtime;
using MoreMountains.Feedbacks;

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

    [SerializeField] private AP_Reference poolRef;
    [SerializeField] private BehaviorTree behaviorTree;
    [SerializeField] private int _cost;

    private bool _dead;

    [Header("Stun")]
    [SerializeField] private MMFeedbacks _stunFeedback;
    [SerializeField] private float _stunDuration;
    [SerializeField] private GameObject _gloryKillTrigger;
    private bool _wasStunned = false;

    // Start is called before the first frame update
    protected override void Awake()
    {
        outlineAlpha = outline.outlineColor.a;
        myRenderer = FindRenderer();
        agent.updatePosition = false;
        stats.RunSpeed = (agent.speed * 2);
    }

    public void OnSpawned()
    {
        GameManager.instance.AliveEnemyCount++;
        Instantiate(RespawnVFX, transform.position, transform.rotation);
        EnableActions();
    }

    private void DisableActions()
    {
        agent.speed = 0;
        behaviorTree.DisableBehavior();
        _wasStunned = false;
    }

    private void EnableActions()
    {
        behaviorTree.EnableBehavior();
        agent.speed = stats.RunSpeed / 2;
        stats.CurrentHealth = stats.Health;
        myCollider.enabled = true;
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
        CheckGroundStatus();
        UpdateAnimator();
        if (!stats.IsAlive())
        {
            Die();
            return;
        }
        Stun();
        
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

    private void Stun()
    {
        if (stats.CurrentHealth < 3 && !_isStunned && !_wasStunned)
        {
            _wasStunned = true;
            int chance = Random.Range(0, 100);
            if (chance >= 65)
            {
                StartCoroutine(OnStunned());
            }
        }
    }

    private IEnumerator OnStunned()
    {
        DisableActions();
        _isStunned = true;
        _gloryKillTrigger.SetActive(true);
        _stunFeedback.PlayFeedbacks();
        yield return new WaitForSeconds(_stunDuration);
        _isStunned = false;
        _gloryKillTrigger.SetActive(false);
        stats.CurrentHealth = 0;
        /*_stunFeedback.StopFeedbacks();
        EnableActions();*/
    }

    private void Die()
    {
        if (!_dead)
        {
            if (_isStunned)
            {
                _isStunned = false;
            }

            myCollider.enabled = false;
            DisableActions();

            animator.SetInteger("Death_Index", Random.Range(0, 4));

            GameManager.instance.AddScore(_cost);
            if (GameManager.instance.AliveEnemyCount == 1)
            {
                GameManager.instance.OnLastEnemyDeath();
            }
            GameManager.instance.AliveEnemyCount--;

            _dead = true;

            StartCoroutine(Despawn());
        }
    }

    private IEnumerator Despawn()
    {
        yield return new WaitForSeconds(respawnTime);
        gameObject.SetActive(false);     
        MF_AutoPool.Despawn(poolRef, 2f);
    }

    public void DespawnOnPlayerDeath()
    {
        gameObject.SetActive(false);
        GameManager.instance.AliveEnemyCount--;
        MF_AutoPool.Despawn(poolRef, 2f);
    }

    private void OnAnimatorMove()
    {
        transform.position = agent.nextPosition;
    }
}
