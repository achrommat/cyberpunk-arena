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
    [SerializeField] private float maxEnergy = 100;

    [HideInInspector] public float currentEnergy;

    [Header("Fire Rate")]
    public float attackDelay = 1;

    [Header("Settings")]
    public GameObject bulletPrefab;
    [SerializeField] private Transform aimPoint;
    [HideInInspector] public Transform shootPosition;

    [Header("FX")]
    [SerializeField] private AudioSource audioSource;
    public GameObject muzzleFlash;
    public Transform muzzleFlashPos;

    private void OnEnable()
    {
        currentEnergy = maxEnergy;
    }

    public void GetScatter(int[] pool, int i)
    {
        shootPosition = aimPoint;
        shootPosition.RotateAround(aimPoint.position, Vector3.up, Random.Range(-scatter, scatter) + pool[i]);
    }

    public void GetScatter()
    {
        shootPosition = aimPoint;
        shootPosition.RotateAround(aimPoint.position, Vector3.up, Random.Range(-scatter, scatter));
    }

    public void PlayShotSound()
    {
        audioSource.PlayOneShot(audioSource.clip);
    }
}
