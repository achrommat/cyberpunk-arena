using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Настройки оружия
    [Header("Main Stats")]
    public new string name = "Weapon Name";
    public float damage = 1;
    public float bulletForce = 1;
    public bool multiShot = false;    
    [SerializeField] private float scatter = 0;
    [SerializeField] private float currentScatter;
    [SerializeField] private float maxEnergy = 100;

    [HideInInspector] public float currentEnergy;

    [Header("Fire Rate")]
    [SerializeField] private float attackDelay = 1;
    public float currentAttackDelay;

    [Header("Settings")]
    public GameObject bulletPrefab;
    [SerializeField] private Transform aimPoint;
    [HideInInspector] public Transform shootPosition;

    [Header("Aiming Cone")]
    public float ConeRadius = 5f;
    [Range(0f, 360f)] public float ConeAngle = 45f;

    [Header("FX")]
    [SerializeField] private AudioSource audioSource;
    public GameObject muzzleFlash;
    public Transform muzzleFlashPos;

    [Header("Devotion")]
    public bool devotion = false;

    private void OnEnable()
    {
        currentEnergy = maxEnergy;
        currentScatter = scatter;
        currentAttackDelay = attackDelay;
    }

    public void GetScatter(int[] pool, int i)
    {
        shootPosition = aimPoint;
        shootPosition.RotateAround(aimPoint.position, Vector3.up, Random.Range(-currentScatter, currentScatter) + pool[i]);
    }

    public void GetScatter()
    {
        shootPosition = aimPoint;
        shootPosition.RotateAround(aimPoint.position, Vector3.up, Random.Range(-currentScatter, currentScatter));
    }

    public void PlayShotSound()
    {
        audioSource.PlayOneShot(audioSource.clip);
    }

    public void HandleDevotion()
    {
        if (currentScatter >= 1f)
        {
            currentScatter -= 0.5f;
        }
        if (currentAttackDelay >= 0.15f)
        {
            currentAttackDelay -= 0.05f;
        }
    }

    public void ResetDevotion()
    {
        currentScatter = scatter;
        currentAttackDelay = attackDelay;
    }
}
