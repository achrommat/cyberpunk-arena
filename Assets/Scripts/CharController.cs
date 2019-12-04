using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    [Header("Sound FX")]

    public float FootStepsRate = 0.4f;
    public float GeneralFootStepsVolume = 1.0f;
    public AudioClip[] Footsteps;
    private float LastFootStepTime = 0.0f;
    private AudioSource Audio;

    [Header("Main")]

    public float speed;
    public FixedJoystick variableJoystick;
    public Joystick variableJoystick2;
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
    bool aiming;
    bool Jump;
    float run;


    private float m_GroundCheckDistance = 0.25f;
    private void Awake()
    {
        charAnimator = GetComponent<Animator>();
        OnGround = true;
    }
    public void FixedUpdate()
    {
        UpdateAnimator();
        CheckGroundStatus();
        MovePosition();
        MoveRotation();
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
            direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
        }
        //if (movement.x > -0.1 && movement.z < 0.1)
        //    movement.x = 0;
        //if (movement.z > -0.1 && movement.z < 0.1)
        //    movement.z = 0;

        rb.MovePosition(transform.position + direction * speed * Time.fixedDeltaTime);

        run = (Mathf.Abs(movement.x) + Mathf.Abs(movement.z)) * 2;
        if (run > 1)
            run = 1;
        if (run < 0.1)
            run = 0;
    }

    void MoveRotation()
    {
        rotation.x = variableJoystick2.Horizontal;
        rotation.z = variableJoystick2.Vertical;

        if ((rotation.x <= -0.1 || rotation.x >= 0.1) || (rotation.z <= -0.1 || rotation.z >= 0.1))
        {
            rotation = new Vector3(rotation.x, 0, rotation.z);
            gameObject.transform.parent.LookAt(gameObject.transform.position + rotation * Time.deltaTime);
            aiming = true;
            Debug.Log("aiming");
        }
        else
        {
            aiming = false;
            movement = new Vector3(movement.x, 0, movement.z);
            gameObject.transform.parent.LookAt(gameObject.transform.position + movement * Time.deltaTime);
        }
        //Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity);
        //rb.MoveRotation(rb.rotation * deltaRotation);//* direction2 * speed * Time.fixedDeltaTime, ForceMode.VelocityChange

    }
    void UpdateAnimator()
    {
        // update the animator parameters
        //charAnimator.SetFloat("Y", variableJoystick2.Vertical);
        //charAnimator.SetFloat("X", variableJoystick2.Horizontal);


        charAnimator.SetFloat("Speed", run);
        if (!Sprinting)
            charAnimator.SetFloat("Speed", 1, AnimatorSprintDampValue, Time.deltaTime);
        else
            charAnimator.SetFloat("Speed", 2.0f, AnimatorRunDampValue, Time.deltaTime);

        charAnimator.SetBool("Aiming", aiming);
       // charAnimator.SetBool("Jump", Jump);

        charAnimator.SetBool("OnGround", OnGround);


        charAnimator.SetBool("Crouch", m_Crouching);
        charAnimator.SetBool("ongroundstay", ongroundstay);

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
            charAnimator.applyRootMotion = true;
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



    /// <summary>
    /// TEST
    /// </summary>
    public void FootStep()
    {
        if (Footsteps.Length > 0 && Time.time >= (LastFootStepTime + FootStepsRate))
        {
            int FootStepAudio = 0;

            if (Footsteps.Length > 1)
            {
                FootStepAudio = Random.Range(0, Footsteps.Length);
            }

            float FootStepVolume = charAnimator.GetFloat("Speed") * GeneralFootStepsVolume;
            if (aiming)
                FootStepVolume *= 0.5f;

            Audio.PlayOneShot(Footsteps[FootStepAudio], FootStepVolume);

           // MakeNoise(FootStepVolume * 10f);
            LastFootStepTime = Time.time;
        }
    }
}
