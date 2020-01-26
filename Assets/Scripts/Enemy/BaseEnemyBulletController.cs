using BehaviorDesigner.Runtime.Tactical;
using UnityEngine;


public class BaseEnemyBulletController : MonoBehaviour
{
    [Header("Stats")]
    public float damage;
    public float speed = 10;
    public float lifetime = 1;
    private float currentLifetime;

    public GameObject hitVFX;
   
    public GameObject bulletPool;
    private GameObject vfxPool;

    public void OnEnable()
    {
        currentLifetime = lifetime;
    }  

    void Update()
    {
        currentLifetime -= Time.fixedDeltaTime;
        if (currentLifetime <= 0)
        {
            Disable(true);
            return;
        }
        GetComponent<Rigidbody>().MovePosition(transform.position + transform.forward * speed * Time.fixedDeltaTime);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Walls"))
        {
            Disable(false);
        }

        IDamageable damageable;
        if ((damageable = other.gameObject.GetComponent(typeof(IDamageable)) as IDamageable) != null)
        {
            damageable.Damage(damage);
            Disable(false);
        }

        //if (other.CompareTag("Player"))
        //{
        //    other.transform.GetChild(0).GetComponent<CharController>().Health -= damage;
        //    Disable(false);            
        //}
    }

    private void Disable(bool isMissed)
    {
        //if (isMissed)

        gameObject.transform.SetParent(bulletPool.transform);
        gameObject.SetActive(false);
    }

}
