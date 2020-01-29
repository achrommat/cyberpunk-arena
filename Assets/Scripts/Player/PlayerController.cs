using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    
    public Transform HealthBar;
    public Joystick moveJoystick;
    public Joystick aimJoystick;
    Rigidbody rb;
    Vector3 m_EulerAngleVelocity;
    Animator charAnimator;
    Vector3 movement;
    Vector3 rotation;
    Vector3 direction;
    float run;
    public float ShootZone = 0.75f;

    public GameObject Weapons;
    public GameObject DeadVFX;
    protected Stats stats;

    bool OnGround;
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
    public float RespawnTime;
    protected float ActualRespTime;
    public GameObject RespawnTarget;
    public GameObject RespawnVFX;

    private void Awake()
    {
        stats = GetComponent<Stats>();
        charAnimator = GetComponent<Animator>();
        OnGround = true;
        Audio = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }
    public void FixedUpdate()
    {
        if(Dead || dash) return; 

        UpdateAnimator();
        CheckGroundStatus();
        Death();
        DeathExtra();

        MovePosition();
        MoveRotation();
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

        Vector3 localMoove = transform.InverseTransformDirection(movement);
        if ((localMoove.x <= -0.3 || localMoove.x >= 0.3) || (localMoove.z <= -0.3 || localMoove.z >= 0.3))
        {
            rb.MovePosition(transform.position + direction * stats.runSpeed * Time.fixedDeltaTime);
            run = (Mathf.Abs(localMoove.x) + Mathf.Abs(localMoove.z));
        }
        else
        {
            rb.useGravity = true;
            transform.localRotation = Quaternion.Euler(Vector3.zero);
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
                    Weapons.transform.GetComponent<WeaponController>().shooting = true;
                else
                    Weapons.transform.GetComponent<WeaponController>().shooting = false;
                //  Target.transform.GetChild(0).gameObject.SetActive(true);

            }
            else
            {
                stats.speedWithAim = 0;
                aiming = false;
                movement = new Vector3(movement.x, 0, movement.z);
                gameObject.transform.LookAt(transform.position + movement * Time.deltaTime);
                Weapons.transform.GetComponent<WeaponController>().shooting = false;
                //    Target.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }

    void CheckGroundStatus()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, m_GroundCheckDistance))
        {
            OnGround = true;
        }
        else
        {
            OnGround = false;
        }
    }
    void Death()
    {
        if (stats.currentHealth <= 0)
        {
            Dead = true;
            if (Weapons != null)
                Weapons.transform.GetComponent<WeaponController>().shooting = false;
        }

        else
        {
            Dead = false;
        }
        Respawn();
    }
    void DeathExtra()
    {
        if (stats.currentHealth <= -50 && DeadExtra == false)
        {
            Instantiate(DeadVFX, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), transform.rotation);
            if (rb == null)
            {
                Destroy(gameObject);
            }

            if (Weapons != null)
            {
                Weapons.transform.GetComponent<WeaponController>().shooting = false;
                Weapons.SetActive(false);
            }

            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            if (gameObject.CompareTag("Enemy"))
            {
                transform.GetComponent<CapsuleCollider>().enabled = false;
                transform.GetComponent<Rigidbody>().useGravity = false;

            }
            else
            {
                transform.GetComponent<CapsuleCollider>().enabled = false;
                if (rb != null)
                    transform.GetComponent<Rigidbody>().useGravity = false;

            }
            if (rb != null)
                rb.velocity = Vector3.zero;
            DeadExtra = true;
            Dead = true;
        }
    }
    void Respawn()
    {
        ActualRespTime -= Time.deltaTime;
        if (ActualRespTime <= 0 && RespawnTarget != null)
        {
            ActualRespTime = RespawnTime;
            stats.currentHealth = 4;
            charAnimator.enabled = false;
            charAnimator.enabled = true;
            Vector3 resp = RespawnTarget.transform.GetChild(Random.Range(0, RespawnTarget.transform.childCount)).transform.position;
            transform.gameObject.transform.position = resp;
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
        charAnimator.SetFloat("Y", Z, 0.1f, Time.deltaTime);
        charAnimator.SetFloat("X", X, 0.1f, Time.deltaTime);

        charAnimator.SetFloat("Run", run);
        charAnimator.SetFloat("Speed", stats.runSpeed);

        if (!Sprinting)
            charAnimator.SetFloat("Speed", 1, AnimatorSprintDampValue, Time.deltaTime);
        else
            charAnimator.SetFloat("Speed", 2.0f, AnimatorRunDampValue, Time.deltaTime);


        charAnimator.SetBool("Aiming", aiming);
        // charAnimator.SetBool("Jump", Jump);

        charAnimator.SetBool("OnGround", OnGround);
        //charAnimator.SetBool("Dance", dance);
        charAnimator.SetBool("Dash", dash);


        charAnimator.SetBool("Dead", Dead);
    }

    /*void HealthController()//считаем сколько сейчас хп
    {
        if (stats.currentHealth < stats.health)
        {
            for (int i = 0; i < HealthBar.childCount; i++)
            {
                if (stats.Health < stats.healthCount && HealthBar.GetChild(i).GetChild(0).GetComponent<Image>().fillAmount > 0)
                {
                    HealthBar.GetChild(i).GetChild(0).GetComponent<Image>().fillAmount -= 0.5f;
                    stats.currentHealth -= 0.5f;
                }
                if (stats.Health < stats.healthCount && HealthBar.GetChild(i).GetChild(0).GetComponent<Image>().fillAmount > 0)// && (HealthBar.childCount - i) > Health
                {
                    HealthBar.GetChild(i).GetChild(0).GetComponent<Image>().fillAmount -= 0.5f;
                    stats.healthCount -= 0.5f;
                }
            }
        }
        if (stats.Health > stats.healthCount)
        {
            for (int i = HealthBar.childCount - 1; i >= 0; i--)
            {
                if (stats.Health > stats.healthCount && HealthBar.GetChild(i).GetChild(0).GetComponent<Image>().fillAmount < 1)
                {
                    HealthBar.GetChild(i).GetChild(0).GetComponent<Image>().fillAmount += 0.5f;
                    stats.healthCount += 0.5f;
                }
                if (stats.Health > stats.healthCount && HealthBar.GetChild(i).GetChild(0).GetComponent<Image>().fillAmount < 1)// && (HealthBar.childCount - i) > Health
                {
                    HealthBar.GetChild(i).GetChild(0).GetComponent<Image>().fillAmount += 0.5f;
                    stats.healthCount += 0.5f;
                }
            }
        }
        if (stats.Health < stats.healthCount)
        {
            for (int i = 0; i < HealthBar.childCount; i++)
            {
                if (stats.Health < stats.healthCount && HealthBar.GetChild(i).GetChild(0).GetComponent<Image>().fillAmount > 0)
                {
                    HealthBar.GetChild(i).GetChild(0).GetComponent<Image>().fillAmount -= 0.5f;
                    stats.healthCount -= 0.5f;
                }
                if (stats.Health < stats.healthCount && HealthBar.GetChild(i).GetChild(0).GetComponent<Image>().fillAmount > 0)// && (HealthBar.childCount - i) > Health
                {
                    HealthBar.GetChild(i).GetChild(0).GetComponent<Image>().fillAmount -= 0.5f;
                    stats.healthCount -= 0.5f;
                }
            }
        }
        if (stats.Health > stats.healthCount)
        {
            for (int i = HealthBar.childCount - 1; i >= 0; i--)
            {
                if (stats.Health > stats.healthCount && HealthBar.GetChild(i).GetChild(0).GetComponent<Image>().fillAmount < 1)
                {
                    HealthBar.GetChild(i).GetChild(0).GetComponent<Image>().fillAmount += 0.5f;
                    stats.healthCount += 0.5f;
                }
                if (stats.Health > stats.healthCount && HealthBar.GetChild(i).GetChild(0).GetComponent<Image>().fillAmount < 1)// && (HealthBar.childCount - i) > Health
                {
                    HealthBar.GetChild(i).GetChild(0).GetComponent<Image>().fillAmount += 0.5f;
                    stats.healthCount += 0.5f;
                }
            }
        }
    }*/
}
