using BehaviorDesigner.Runtime.Tactical;
using UnityEngine;
using MoreMountains.TopDownEngine;

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

    [Header("Aim Assist")]
    public Transform autoAimStartPoint;
    [SerializeField] private Transform aimPoint;
    [SerializeField] private float aimAssistSize = 1f;
    [SerializeField] private WeaponLaserSight _laserSight;

    public Transform aimingCameraOffsetPoint;

    protected void FixedUpdate()
    {
        if (!stats.IsAlive())
        {
            return;
        }
        Move();
        Rotate();
        Shoot();
    }

    protected override void Update()
    {
        base.Update();
        GetMovementJoystickInput();
        GetAimingJoystickInput();
        _laserSight.LaserActive(aiming && stats.IsAlive());
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

        // Учитывание мертвой зоны начала прицеливания
        if (((localMove.x <= -aimDeadZone || localMove.x >= aimDeadZone) || (localMove.z <= -aimDeadZone || localMove.z >= aimDeadZone)))
        {
            aiming = true;
            stats.speedWithAim = -3;
            return;
        }
        aiming = false;
        stats.speedWithAim = 0;
    }

    private void Rotate()
    {
        // При прицеливании учитывается направление поворота правого стика
        // При обычном беге - левого
        if (aiming)
        {
            transform.LookAt(transform.position + rotation * Time.deltaTime);
            return;
        }
        transform.LookAt(transform.position + movement * Time.deltaTime);
    }

    private void Shoot()
    {
        if (aiming && stats.IsAlive())
        {
            AimAssist();

            // Учитывание мертвой зоны стрельбы
            if (Mathf.Abs(rotation.x) + Mathf.Abs(rotation.z) >= shootZone)
            {
                shootable.Attack();
            }           
        }
    }    

    private void AimAssist()
    {
        // Если в сферу попадает враг, то меняем направление прцела на него
        // Иначе обнуляем направление прицела
        RaycastHit hit;
        LayerMask mask = LayerMask.GetMask("Enemy");
        Debug.DrawRay(autoAimStartPoint.position, autoAimStartPoint.transform.forward * 100f, Color.green);
        if (Physics.SphereCast(autoAimStartPoint.position, aimAssistSize, transform.forward, out hit, 100f, mask))
        {
            IDamageable damageable;
            if ((damageable = hit.collider.gameObject.GetComponent(typeof(IDamageable)) as IDamageable) != null)
            {
                if (damageable.IsAlive())
                {
                    aimPoint.LookAt(hit.collider.bounds.center);
                }
                else
                {
                    aimPoint.localRotation = Quaternion.identity;
                    return;
                }                
            }
        }   
        else
        {
            aimPoint.localRotation = Quaternion.identity;
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
