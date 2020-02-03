using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private Stats stats;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Animator animator;
    private bool onGround;

    public float movementDeadZone;

    public Transform HealthBar;
    public Joystick moveJoystick;
    public Joystick aimJoystick;
    
    Vector3 m_EulerAngleVelocity;
    
    Vector3 movement;
    Vector3 rotation;
    Vector3 direction;
    float run;
    public float ShootZone = 0.75f;

    public GameObject Weapons;
    public GameObject DeadVFX;
    

    
    public bool aiming;
    bool Jump;
    bool HaveTarget;

    private float m_GroundCheckDistance = 0.25f;


    //это всё будет в статах.. ?
    public bool DeadExtra;
    public bool dash;
    public bool Dead;

    [Header("RunAnim")]
    public bool Sprinting = false;
    public float AnimatorRunDampValue = 0.25f;
    public float AnimatorSprintDampValue = 0.2f;
    public float AnimatorAimingDampValue = 0.1f;

    [Header("SOUND FX")]
    public float FootStepsRate = 0.2f;
    public float GeneralFootStepsVolume = 1.0f;
    private float LastFootStepTime = 0.0f;
    public AudioClip[] Footsteps;
    private AudioSource Audio;

    [Header("RESPAWN")]
    public float respawnTime;
    protected float actualRespawnTime;
    private Vector3 respawnTarget;
    public GameObject RespawnVFX;

    private void Awake()
    {
        actualRespawnTime = respawnTime;
        respawnTarget = transform.position;
        onGround = true;
        Audio = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {  
        if (!stats.IsAlive())
        {
            Respawn();
            return;
        }
        MovePosition();
        MoveRotation();
    }

    private void Update()
    {
        UpdateAnimator();
        CheckGroundStatus();
    }

    void MovePosition()
    {
        direction = Vector3.forward * movement.z + Vector3.right * movement.x;
        direction = direction.normalized;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            movement.x = Input.GetAxis("Horizontal");
            movement.z = Input.GetAxis("Vertical");
        }
        else
        {
            movement.x = moveJoystick.Horizontal;
            movement.z = moveJoystick.Vertical;
        }

        Vector3 localMove = transform.InverseTransformDirection(movement);
        if ((localMove.x <= -movementDeadZone || localMove.x >= movementDeadZone) || (localMove.z <= -movementDeadZone || localMove.z >= movementDeadZone))
        {
            rb.MovePosition(transform.position + direction * stats.runSpeed * Time.fixedDeltaTime);
            run = 1;
        }
        else
        {
            run = 0;
        }

    }

    void MoveRotation()
    {
        rotation.x = aimJoystick.Horizontal;
        rotation.z = aimJoystick.Vertical;
        rotation = new Vector3(rotation.x, 0, rotation.z);

        //variableJoystick.DeadZone = 0.1f;

        //variableJoystick2.HandleRange = 2;
        Vector3 localMoove = transform.InverseTransformDirection(rotation);

        if (HaveTarget == false && !dash)
        {
            if (((localMoove.x <= -0.2 || localMoove.x >= 0.2) || (localMoove.z <= -0.2 || localMoove.z >= 0.2)))
            {
                stats.speedWithAim = -3;
                gameObject.transform.LookAt(transform.position + rotation * Time.deltaTime);

                aiming = true;
                if (Mathf.Abs(rotation.x) + Mathf.Abs(rotation.z) >= ShootZone)
                    transform.GetComponent<WeaponController>().shooting = true;
                else
                    transform.GetComponent<WeaponController>().shooting = false;
                //  Target.transform.GetChild(0).gameObject.SetActive(true);

            }
            else
            {
                stats.speedWithAim = 0;
                aiming = false;
                movement = new Vector3(movement.x, 0, movement.z);
                gameObject.transform.LookAt(transform.position + movement * Time.deltaTime);
                transform.GetComponent<WeaponController>().shooting = false;
                //    Target.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }

    // TODO: перенести на коллижн с тегом Ground
    void CheckGroundStatus()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, m_GroundCheckDistance))
        {
            onGround = true;
            return;
        }
        onGround = false;
    }


    void Respawn()
    {
        actualRespawnTime -= Time.deltaTime;
        if (actualRespawnTime <= 0 && respawnTarget != null)
        {
            actualRespawnTime = respawnTime;
            stats.currentHealth = 4;
            animator.enabled = false;
            animator.enabled = true;
            //Vector3 resp = RespawnTarget.transform.GetChild(Random.Range(0, RespawnTarget.transform.childCount)).transform.position;
            transform.gameObject.transform.position = respawnTarget;
            Instantiate(RespawnVFX, transform.position, transform.rotation);
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(true);
            transform.GetComponent<CapsuleCollider>().enabled = true;
            transform.GetComponent<Rigidbody>().useGravity = true;
            Weapons.SetActive(true);
            DeadExtra = false;

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


           // Audio.PlayOneShot(Footsteps[FootStepAudio], 1f);

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

    void UpdateAnimator()
    {
        Vector3 localMoove = transform.InverseTransformDirection(movement);
        float Z = localMoove.z;
        float X = localMoove.x;

        // update the animator parameters
        animator.SetFloat("Y", Z, 0.1f, Time.deltaTime);
        animator.SetFloat("X", X, 0.1f, Time.deltaTime);

        animator.SetFloat("Run", run);
        animator.SetFloat("Speed", stats.runSpeed);

        if (!Sprinting)
            animator.SetFloat("Speed", 1, AnimatorSprintDampValue, Time.deltaTime);
        else
            animator.SetFloat("Speed", 2.0f, AnimatorRunDampValue, Time.deltaTime);


        animator.SetBool("Aiming", aiming);

        animator.SetBool("OnGround", onGround);
        animator.SetBool("Dash", dash);


        animator.SetBool("Dead", !stats.IsAlive());
    }
}
