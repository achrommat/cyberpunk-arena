using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharController : MonoBehaviour
{
    [Header("Stats")]
    public float Health = 4; 
    public float healthCount;
    public float defence = 4;
    public float defenceCount;
    public float Armor;
    public bool Dead;
    public bool DeadExtra;
    public float RespawnTime;
    float ActualRespTime;
    public float ShootZone = 0.75f;
    bool autoaim = false;
    public bool dance = false;
    public Transform HealthBar;
    public Transform DefenceBar;




    [Header("Dash")]
    public float dashPower = 5000;
    public float dashTime = 1.5f;
    public float dashCD = 6f;
    float dashCDCurrentTime;
    public bool dash = false;
    public GameObject dashVFX;
    public Image DashUI;

    [Header("WallRun")]
    public bool wallRun;
    public Quaternion wallRunRotation;

    [Header("Sound FX")]
    public float FootStepsRate = 0.2f;
    public float GeneralFootStepsVolume = 1.0f;
    public AudioClip[] Footsteps;
    private float LastFootStepTime = 0.0f;
    private AudioSource Audio;

    [Header("Main")]

    private float speed;
    public float startspeed;
    public float speedmody;
    public float speedaim;
    public float speedsetting = 1;
    public float RotationDeadZone = 0.3f;
    public float MovementDeadZone = 0.3f;
    public Joystick variableJoystick;
    public Joystick variableJoystick2;
    public Joystick ShootJoystick;

    public Rigidbody rb;
    Vector3 m_EulerAngleVelocity;
    Animator charAnimator;

    public Vector3 movement;
    public Vector3 rotation;
    Vector3 direction;

    public bool Sprinting = false;
    public float AnimatorRunDampValue = 0.25f;
    public float AnimatorSprintDampValue = 0.2f;
    public float AnimatorAimingDampValue = 0.1f;

    bool m_Crouching;
    bool OnGround;
    bool ongroundstay;
    public bool aiming;
    bool Jump;
    bool HaveTarget;
    public float run;


    [Header("Guns")]
    public GameObject GunHeand;
    public GameObject Weapons;

    [Header("Target")]
    public GameObject Target;
    public GameObject RespawnTarget;

    [Header("VFX")]
    public GameObject RespawnVFX;
    public GameObject DeadVFX;
    public bool InControll;


    // stats
    private Stats stats;

    private float m_GroundCheckDistance = 0.25f;
    private void Awake()
    {
        charAnimator = GetComponent<Animator>();
        OnGround = true;
        Audio = GetComponent<AudioSource>();
        ActualRespTime = RespawnTime;
        stats = GetComponent<Stats>();

        // stats
        healthCount = stats.health;// Текущее хп = максимальному

        defenceCount = defence;
    }
    public void FixedUpdate()
    {
        HealthController();
        UpdateAnimator();
        CheckGroundStatus();
        Death();
        DeathExtra();

        if (!Dead && InControll && !dash)
        {
            MovePosition();
            //   MoveRotation();
        }
        if (Dead)
        {
            Respawn();
        }
        speed = (startspeed + speedaim + speedmody);//* speedsetting;
        if (dash)
        {
            StartCoroutine(Dash());
        }
        if (DashUI.fillAmount < 1)
        {
            DashUI.fillAmount += 1 / dashCDCurrentTime * Time.fixedDeltaTime;
        }
        else
        {
            DashUI.transform.parent.GetChild(1).gameObject.SetActive(true);
           // transform.GetChild(1).GetComponent<Outline>().OutlineColor = new Color32(0, 227, 255, 50);

        }
    }
    public void Update()
    {
        if (Dead == false && InControll)
        {
             MoveRotation();
        }
    }
    void MovePosition()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            movement.x = Input.GetAxis("Horizontal");
            movement.z = Input.GetAxis("Vertical");

            direction = Vector3.forward * movement.z + Vector3.right * movement.x;
            direction = direction.normalized;
        }
        else
        {
            movement.x = variableJoystick.Horizontal;
            movement.z = variableJoystick.Vertical;
            if (wallRun)
            {
                direction = Vector3.forward;
                transform.rotation = Quaternion.Euler(wallRunRotation.x, wallRunRotation.y + 90, wallRunRotation.z);
            }
            else
            {
                direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
            }
        }

        Vector3 localMoove = transform.InverseTransformDirection(movement);
        if ((localMoove.x <= -0.3 || localMoove.x >= 0.3) || (localMoove.z <= -0.3 || localMoove.z >= 0.3))
        {
            GetComponent<Animator>().applyRootMotion = false;
            rb.MovePosition(transform.position + direction * speed * Time.fixedDeltaTime);
            run = (Mathf.Abs(localMoove.x) + Mathf.Abs(localMoove.z));
        }
        else
        {
            rb.useGravity = true;
            transform.localRotation = Quaternion.Euler(Vector3.zero);
            run = 0;
        }

    }
    public void AutoaimShootON()
    {
        // yield return (new WaitForSeconds(1));
        speedaim = -3;
        aiming = true;
        Weapons.transform.GetComponent<WeaponController>().shooting = true;
        autoaim = true;

    }

    void HealthController()//считаем сколько сейчас хп
    {
        // stats
        Health = stats.currentHealth;


        //for(int i = 0; i>HealthBar.childCount; i++) // 
        //{
        //    if (healthCount > 0 && HealthBar.GetChild(i).GetComponent<Image>().fillAmount < 1)
        //    {
        //        HealthBar.GetChild(i).GetComponent<Image>().fillAmount += 0.5f;
        //        healthCount -= 0.5f;
        //    }
        //    if (healthCount > 0 && HealthBar.GetChild(i).GetComponent<Image>().fillAmount < 1)
        //    {
        //        HealthBar.GetChild(i).GetComponent<Image>().fillAmount += 0.5f;
        //        healthCount -= 0.5f;
        //    }
        //}
        if (Health < healthCount)
        {
            for (int i = 0; i < HealthBar.childCount; i++)
            {
                if (Health < healthCount && HealthBar.GetChild(i).GetChild(0).GetComponent<Image>().fillAmount > 0)
                {
                    HealthBar.GetChild(i).GetChild(0).GetComponent<Image>().fillAmount -= 0.5f;
                    healthCount -= 0.5f;
                }
                if (Health < healthCount && HealthBar.GetChild(i).GetChild(0).GetComponent<Image>().fillAmount > 0)// && (HealthBar.childCount - i) > Health
                {
                    HealthBar.GetChild(i).GetChild(0).GetComponent<Image>().fillAmount -= 0.5f;
                    healthCount -= 0.5f;
                }
            }
        }
        if (Health > healthCount)
        {
            for (int i = HealthBar.childCount - 1; i >= 0; i--)
            {
                if (Health > healthCount && HealthBar.GetChild(i).GetChild(0).GetComponent<Image>().fillAmount < 1)
                {
                    HealthBar.GetChild(i).GetChild(0).GetComponent<Image>().fillAmount += 0.5f;
                    healthCount += 0.5f;
                }
                if (Health > healthCount && HealthBar.GetChild(i).GetChild(0).GetComponent<Image>().fillAmount < 1)// && (HealthBar.childCount - i) > Health
                {
                    HealthBar.GetChild(i).GetChild(0).GetComponent<Image>().fillAmount += 0.5f;
                    healthCount += 0.5f;
                }
            }
        }
        if (Health < healthCount)
        {
            for (int i = 0; i < HealthBar.childCount; i++)
            {
                if (Health < healthCount && HealthBar.GetChild(i).GetChild(0).GetComponent<Image>().fillAmount > 0)
                {
                    HealthBar.GetChild(i).GetChild(0).GetComponent<Image>().fillAmount -= 0.5f;
                    healthCount -= 0.5f;
                }
                if (Health < healthCount && HealthBar.GetChild(i).GetChild(0).GetComponent<Image>().fillAmount >0)// && (HealthBar.childCount - i) > Health
                {
                    HealthBar.GetChild(i).GetChild(0).GetComponent<Image>().fillAmount -= 0.5f;
                    healthCount -= 0.5f;
                }
            }
        }
        if (Health > healthCount)
        {
            for (int i = HealthBar.childCount-1; i >=0; i--)
            {
                if (Health > healthCount && HealthBar.GetChild(i).GetChild(0).GetComponent<Image>().fillAmount < 1)
                {
                    HealthBar.GetChild(i).GetChild(0).GetComponent<Image>().fillAmount += 0.5f;
                    healthCount += 0.5f;
                }
                if (Health > healthCount && HealthBar.GetChild(i).GetChild(0).GetComponent<Image>().fillAmount < 1)// && (HealthBar.childCount - i) > Health
                {
                    HealthBar.GetChild(i).GetChild(0).GetComponent<Image>().fillAmount += 0.5f;
                    healthCount += 0.5f;
                }
            }
        }
    }

    public void AutoaimShootOFF()
    {
        // yield return (new WaitForSeconds(1));
        speedaim = +3;
        aiming = false;
        Weapons.transform.GetComponent<WeaponController>().shooting = false;
        autoaim = false;

    }
    void MoveRotation()
    {
        rotation.x = variableJoystick2.Horizontal;
        rotation.z = variableJoystick2.Vertical;
        rotation = new Vector3(rotation.x, 0, rotation.z);

        //variableJoystick.DeadZone = 0.1f;

        //variableJoystick2.HandleRange = 2;
        Vector3 localMoove = transform.InverseTransformDirection(rotation);

        if (HaveTarget == false && !autoaim && !wallRun && !dash)
        {
            if (((localMoove.x <= -0.2 || localMoove.x >= 0.2) || (localMoove.z <= -0.2 || localMoove.z >= 0.2)))
            {
                speedaim = -3;
                gameObject.transform.parent.LookAt(transform.parent.position + rotation * Time.deltaTime);

                aiming = true;
                if (Mathf.Abs(rotation.x) + Mathf.Abs(rotation.z) >= ShootZone)
                    Weapons.transform.GetComponent<WeaponController>().shooting = true;
                else
                    Weapons.transform.GetComponent<WeaponController>().shooting = false;
                //  Target.transform.GetChild(0).gameObject.SetActive(true);

            }
            else
            {
                speedaim = 0;
                aiming = false;
                movement = new Vector3(movement.x, 0, movement.z);
                gameObject.transform.parent.LookAt(transform.parent.position + movement * Time.deltaTime);
                Weapons.transform.GetComponent<WeaponController>().shooting = false;
                //    Target.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
        else
        {

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
        charAnimator.SetFloat("Speed", speed);

        if (!Sprinting)
            charAnimator.SetFloat("Speed", 1, AnimatorSprintDampValue, Time.deltaTime);
        else
            charAnimator.SetFloat("Speed", 2.0f, AnimatorRunDampValue, Time.deltaTime);


        charAnimator.SetBool("Aiming", aiming);
        // charAnimator.SetBool("Jump", Jump);

        charAnimator.SetBool("OnGround", OnGround);
        charAnimator.SetBool("Dance", dance);
        charAnimator.SetBool("Dash", dash);


        charAnimator.SetBool("Crouch", m_Crouching);
        charAnimator.SetBool("ongroundstay", ongroundstay);
        charAnimator.SetBool("Dead", Dead);
        charAnimator.SetBool("WallRun", wallRun);


    }
    void CheckGroundStatus()
    {
        RaycastHit hitInfo;
#if UNITY_EDITOR
        // helper to visualise the ground check ray in the scene view
        //Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * m_GroundCheckDistance));
#endif
        // 0.1f is a small offset to start the ray from inside the character
        // it is also good to note that the transform position in the sample assets is at the base of the character
        if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, m_GroundCheckDistance))
        {
            OnGround = true;
            //    charAnimator.applyRootMotion = true;
        }
        else
        {
            OnGround = false;
            //charAnimator.applyRootMotion = false;
        }
        ////test
        //OnGround = true;
        //charAnimator.applyRootMotion = true;


    }
    void Death()
    {
        if (Health <= 0)
        {
            Dead = true;
            if(Weapons !=null)
              Weapons.transform.GetComponent<WeaponController>().shooting = false;
        }

        else
        {
            Dead = false;
        }
    }
    void DeathExtra()
    {
        if (Health <= -50 && DeadExtra == false)
        {
            Instantiate(DeadVFX, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), transform.rotation);
            if (rb== null)
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
                transform.parent.GetComponent<CapsuleCollider>().enabled = false;
                if (rb != null)
                    transform.parent.GetComponent<Rigidbody>().useGravity = false;

            }
            if(rb !=null)
             rb.velocity = Vector3.zero;
            DeadExtra = true;
            Dead = true;
        }
    }
    void Respawn()
    {
        ActualRespTime -= Time.deltaTime;
        if (ActualRespTime <= 0 && RespawnTarget !=null)
        {
            ActualRespTime = RespawnTime;
            Health = 4;
            charAnimator.enabled = false;
            charAnimator.enabled = true;
            Vector3 resp = RespawnTarget.transform.GetChild(Random.Range(0, RespawnTarget.transform.childCount)).transform.position;
            transform.parent.gameObject.transform.position = resp;
            Instantiate(RespawnVFX, transform.position, transform.rotation);
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(true);
            if (gameObject.CompareTag("Enemy"))
            {
                transform.GetComponent<CapsuleCollider>().enabled = true;
                transform.GetComponent<Rigidbody>().useGravity = true;
            }
            else
            {
                transform.parent.GetComponent<CapsuleCollider>().enabled = true;
                transform.parent.GetComponent<Rigidbody>().useGravity = true;
            }
            Weapons.SetActive(true);
            DeadExtra = false;

        }
    }
    public void FootStep()
    {
        // if (Footsteps.Length > 0 && Time.time >= (LastFootStepTime + FootStepsRate))
        if (!aiming)
        {
            int FootStepAudio = 0;

            if (Footsteps.Length > 1)
            {
                FootStepAudio = Random.Range(0, Footsteps.Length);
            }

            //float FootStepVolume = charAnimator.GetFloat("Speed") * GeneralFootStepsVolume;
            //if (aiming)
            //    FootStepVolume *= 0.5f;

            Audio.PlayOneShot(Footsteps[FootStepAudio], 1f);

            // MakeNoise(FootStepVolume * 10f);
            //  LastFootStepTime = Time.time;
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
    public void DashStart()
    {
        if(DashUI.fillAmount >=1)
        {
            DashUI.transform.parent.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(1).GetComponent<Outline>().OutlineColor = new Color32(0, 255, 6, 30);
            dash = true;
            DashUI.fillAmount = 0;
            Debug.Log("DASH");
        }
    }
    public IEnumerator Dash()
    {
        rb.velocity = Vector3.zero;
        dashVFX.SetActive(true);
        rb.AddForce(transform.forward * dashPower);
        yield return new WaitForSeconds(dashTime);
        rb.velocity = Vector3.zero;
        dashCDCurrentTime = dashCD;
        dash = false;
       // yield return new WaitForSeconds(0.5f);
        dashVFX.SetActive(false);
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("WallRun"))
        {
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
            wallRun = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("WallRun"))
        {
            rb.useGravity = true;
            transform.localRotation = Quaternion.Euler(Vector3.zero);
            rb.velocity = Vector3.zero;
            wallRunRotation = other.transform.rotation;
            wallRun = false;
        }
    }
}
