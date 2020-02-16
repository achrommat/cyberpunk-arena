using BehaviorDesigner.Runtime.Tactical;
using UnityEngine;

public class BaseCharacterController : MonoBehaviour
{
    [Header("Links")]
    public Stats stats;
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected Animator animator;
    [SerializeField] protected Shootable shootable;
    public WeaponController weaponController;

    [Header("Respawn")]
    [SerializeField] protected float respawnTime;
    protected float actualRespawnTime;
    protected Vector3 respawnTarget;
    [SerializeField] protected GameObject RespawnVFX;

    [Header("Parameters")]
    public bool aiming;
    protected bool dash;
    protected bool HaveTarget;
    protected float run;
    protected bool onGround;

    [Header("SOUND FX")]
    public float FootStepsRate = 0.2f;
    public float GeneralFootStepsVolume = 1.0f;
    protected float LastFootStepTime = 0.0f;
    public AudioClip[] Footsteps;
    protected AudioSource Audio;

    protected float m_GroundCheckDistance = 0.25f;
    public Transform HealthBar;

    protected virtual void Awake()
    {
        actualRespawnTime = respawnTime;
        respawnTarget = transform.position;
        onGround = true;
        Audio = GetComponent<AudioSource>();
    }

    protected virtual void Update()
    {
        CheckGroundStatus();
        UpdateAnimator();
        if (!stats.IsAlive())
        {
            Respawn();
            return;
        }
    }

    // TODO: перенести на коллижн с тегом Ground
    protected void CheckGroundStatus()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, m_GroundCheckDistance))
        {
            onGround = true;
            return;
        }
        onGround = false;
    }

    protected virtual void Respawn()
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

    public void FootStep()
    {
        if (!aiming)
        {
            int FootStepAudio = 0;
            if (Footsteps.Length > 1)
            {
                FootStepAudio = Random.Range(0, Footsteps.Length);
            }
        }
        else
        {
            if (Footsteps.Length > 0 && Time.time >= (LastFootStepTime + FootStepsRate))
            {
                int FootStepAudio = 0;
                if (Footsteps.Length > 1)
                {
                    FootStepAudio = Random.Range(0, Footsteps.Length);
                }
                Audio.PlayOneShot(Footsteps[FootStepAudio], 1f);
                LastFootStepTime = Time.time;
            }
        }
    }

    protected virtual void UpdateAnimator()
    {
        // update the animator parameters        
        animator.SetFloat("Run", run);
        animator.SetFloat("Speed", stats.currentRunSpeed);
        animator.SetBool("Aiming", aiming);
        animator.SetBool("OnGround", onGround);
        animator.SetBool("Dash", dash);
        animator.SetBool("Dead", !stats.IsAlive());
    }
}
