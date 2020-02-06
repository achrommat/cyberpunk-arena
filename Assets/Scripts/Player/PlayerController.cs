using BehaviorDesigner.Runtime.Tactical;
using UnityEngine;

public class PlayerController : BaseCharacterController
{
    [Header("Joysticks")]
    public float movementDeadZone;
    public float aimDeadZone;
    public float shootZone;
    [SerializeField] private Joystick movementJoystick;
    [SerializeField] private Joystick aimJoystick;
    private Vector3 movement;
    private Vector3 rotation;
    private Vector3 direction;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        Move();
        Shoot();
    }

    protected override void Update()
    {
        base.Update();
        GetMovementJoystickInput();
        GetAimingJoystickInput();               
    }

    private void GetMovementJoystickInput()
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
            movement.x = movementJoystick.Horizontal;
            movement.z = movementJoystick.Vertical;
        }
        Vector3 localMove = transform.InverseTransformDirection(movement);
        if ((localMove.x <= -movementDeadZone || localMove.x >= movementDeadZone) || (localMove.z <= -movementDeadZone || localMove.z >= movementDeadZone))
        {            
            run = 1;
            return;
        }
        run = 0;
    }

    private void Move()
    {
        if (run > 0)
        {
            rb.MovePosition(transform.position + direction * stats.runSpeed * Time.fixedDeltaTime);
        }        
    }

    void GetAimingJoystickInput()
    {
        rotation.x = aimJoystick.Horizontal;
        rotation.z = aimJoystick.Vertical;
        rotation = new Vector3(rotation.x, 0, rotation.z);

        Vector3 localMove = transform.InverseTransformDirection(rotation);
        if (HaveTarget == false && !dash)
        {
            if (((localMove.x <= -aimDeadZone || localMove.x >= aimDeadZone) || (localMove.z <= -aimDeadZone || localMove.z >= aimDeadZone)))
            {
                aiming = true;
                stats.speedWithAim = -3;
                gameObject.transform.LookAt(transform.position + rotation * Time.deltaTime);

            }
            else
            {
                aiming = false;
                stats.speedWithAim = 0;
                movement = new Vector3(movement.x, 0, movement.z);
                gameObject.transform.LookAt(transform.position + movement * Time.deltaTime);
            }
        }
    }

    private void Shoot()
    {
        if (aiming && stats.IsAlive())
        {
            if (Mathf.Abs(rotation.x) + Mathf.Abs(rotation.z) >= shootZone)
            {
                shootable.Attack();
            }
        }
    }

    protected override void UpdateAnimator()
    {
        Vector3 localMove = transform.InverseTransformDirection(movement);
        float z = localMove.z;
        float x = localMove.x;
        animator.SetFloat("Y", z, 0.1f, Time.deltaTime);
        animator.SetFloat("X", x, 0.1f, Time.deltaTime);

        base.UpdateAnimator();
    }
}
