using BehaviorDesigner.Runtime.Tactical;
using UnityEngine;
using MoreMountains.TopDownEngine;
using MoreMountains.Tools;
using UnityEngine.Events;
using System.Collections;

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

    public Transform aimingCameraOffsetPoint;
    [HideInInspector] public bool CanShoot;

    [HideInInspector] public bool ShouldGetPerks = false;

    public UnityEvent DeathEvent;

    public PlayerCharacter Character;

    [SerializeField] private PlayerDash _dash;

    public bool IsInvulnerable;

    protected void FixedUpdate()
    {
        if (!stats.IsAlive())
        {
            return;
        }
        Move();
        Rotate();
        CanShoot = aiming && stats.IsAlive() && !Dash && !GloryKill;
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
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            direction = direction.normalized;
            movement.x = Input.GetAxis("Horizontal");
            movement.z = Input.GetAxis("Vertical");
        }
        else
        {
            movement.x = movementJoystick.Horizontal;
            movement.z = movementJoystick.Vertical;
        }
        run = movement.magnitude;
    }

    private void Move()
    {
        if (Dash || GloryKill)
        {
            return;
        }

        rb.MovePosition(transform.position + direction * stats.CurrentRunSpeed * Time.fixedDeltaTime);
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
            return;
        }
        aiming = false;
    }

    private void Rotate()
    {
        if (Dash || GloryKill)
        {
            return;
        }
        // При прицеливании учитывается направление поворота правого стика
        // При обычном беге - левого
        if (aiming)
        {
            transform.LookAt(transform.position + rotation * Time.fixedDeltaTime);
            return;
        }
        transform.LookAt(transform.position + movement * Time.fixedDeltaTime);
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

    protected override void Respawn()
    {
        actualRespawnTime -= Time.deltaTime;
        if (actualRespawnTime <= 0 && respawnTarget != null)
        {
            actualRespawnTime = respawnTime;
            RestartLevel();
        }
    }

    public void RestartLevel()
    {
        DeathEvent.Invoke();
        stats.CurrentHealth = stats.Health;
        transform.position = respawnTarget;
        Instantiate(RespawnVFX, transform.position, transform.rotation);
        ShouldGetPerks = true;
    }

    public void TemporaryInvulnerability()
    {
        IsInvulnerable = true;
        StartCoroutine(ResetInvulnerability());
    }

    private IEnumerator ResetInvulnerability()
    {
        yield return new WaitForSeconds(0.5f);
        IsInvulnerable = false;
    }

}
