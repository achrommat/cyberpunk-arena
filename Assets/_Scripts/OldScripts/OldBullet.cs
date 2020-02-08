using BehaviorDesigner.Runtime.Tactical;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OldBullet : MonoBehaviour
{
    // TODO - переписать нахуй все здесь
    public LayerMask collisionMask;
    public float damage;
    public float speed = 60;
    public GameObject HitVFX;
    public GameObject HitDecal;
    public float lifetime = 1;
    public int through = 0;
    int currentThrough = 0;
    float lifetimer;
    public int ricochet = 0;
    public Vector3 hitpos;
    public bool sawblade;
    public GameObject BulletPool;
    public GameObject VFXpool;
    bool hit = false;
    public float hittime = 1;


    public void OnEnable()
    {
        hittime = 1;
        currentThrough = through;
        lifetimer = lifetime;
        hit = false;
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(false);
        transform.GetChild(3).gameObject.SetActive(false);
        transform.GetChild(4).gameObject.SetActive(false);

        transform.GetComponent<MeshRenderer>().enabled = true;
    }

    void FixedUpdate()
    {
        if (hit == true)
        {
            hittime -= Time.deltaTime;
            if (hittime <= 0)
            {
                gameObject.transform.SetParent(BulletPool.transform);
                gameObject.SetActive(false);
            }
        }

        lifetimer -= Time.fixedDeltaTime;
        if (sawblade == true)
        {
            if (ricochet <= 0 || (lifetimer <= 0 && ricochet == 1))
            {
                transform.GetChild(1).gameObject.SetActive(true);
                transform.GetChild(2).gameObject.SetActive(false);
                // Destroy(gameObject,1f);
                lifetimer = 0;
                gameObject.transform.SetParent(BulletPool.transform);
                gameObject.SetActive(false);


            }
            else if (lifetimer <= 0 && hittime <= 0)
            {
                // Destroy(gameObject);
                gameObject.transform.SetParent(BulletPool.transform);
                gameObject.SetActive(false);
            }
        }
        else if (sawblade == false)
        {
            if (lifetimer <= 0 && !hit)
            {
                // Destroy(gameObject);
                gameObject.transform.SetParent(BulletPool.transform);
                gameObject.SetActive(false);
            }
        }



        if (lifetimer > 0 && hit == false)
        {
            GetComponent<Rigidbody>().MovePosition(transform.position + transform.forward * speed * Time.fixedDeltaTime);
            if (ricochet > 0)
            {
                Ray ray = new Ray(transform.position, transform.forward);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, Time.deltaTime * speed + .1f))
                {
                    Vector3 reflectDir = Vector3.Reflect(ray.direction, hit.normal);
                    float rot = 90 - Mathf.Atan2(reflectDir.z, reflectDir.x) * Mathf.Rad2Deg;
                    transform.eulerAngles = new Vector3(0, rot, 0);
                    // hitpos = hit.point;
                    if (sawblade)
                    {
                        GameObject fx = Instantiate(HitVFX, hit.point, transform.rotation);
                        damage *= Mathf.CeilToInt(1.5f);
                        lifetimer += 0.4f;
                        speed += 2;

                        if (ricochet == 2)
                        {
                            transform.GetChild(0).GetComponent<TrailRenderer>().startColor = Color.yellow;
                        }

                        if (ricochet == 1)
                        {
                            transform.GetChild(0).GetComponent<TrailRenderer>().startColor = Color.red;
                        }


                        if (ricochet < 0)
                        {
                            Debug.Log("У СУКА");

                        }
                    }
                }
            }
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        IDamageable damageable;
        if ((damageable = other.gameObject.GetComponent(typeof(IDamageable)) as IDamageable) != null && !other.CompareTag("Player"))
        {
            damageable.Damage(damage);
            transform.GetChild(1).gameObject.SetActive(true);
            // transform.GetChild(0).gameObject.SetActive(false);
            if (currentThrough <= 0)
            {
                hit = true;
                transform.GetComponent<MeshRenderer>().enabled = false;
            }
            else
            {
                currentThrough -= 1;
                // transform.GetChild(1).gameObject.SetActive(false);
            }
            //Disable(false);
        }

        if (other.CompareTag("Walls"))
        {
            if (ricochet > 0)
            {
                Vector3 reflectDir = Vector3.Reflect(transform.position, other.transform.position).normalized;
                float rot = 90 - Mathf.Atan2(reflectDir.z, reflectDir.x) * Mathf.Rad2Deg;
                transform.eulerAngles = new Vector3(0, rot, 0);
                hit = true;
                //VFXpool.transform.GetChild(0).GetChild(0).SetParent(null);
                //VFXpool.transform.GetChild(0).GetChild(0).transform.position = hitpos;
                //VFXpool.transform.GetChild(0).GetChild(0).transform.rotation = Quaternion.LookRotation(transform.position, other.transform.position);
                //VFXpool.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                // GameObject hit = Instantiate(HitVFX, hitpos, Quaternion.LookRotation(transform.position, other.transform.position));
                ricochet--;
            }
            else
            {
                //      transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(2).gameObject.SetActive(true);
                transform.GetComponent<MeshRenderer>().enabled = false;
                hit = true;
                //тут надо добить поворот попадения - Quaternion или забить хуй (пока второй вариант)
                //возможно надо делать через рейкаст (пример в PrBullet)
            }
        }
        /*if (other.CompareTag("Enemy"))
        {
            if (other.transform.GetComponent<CharController>())
                other.transform.GetComponent<CharController>().Health -= damage;
            else
                other.transform.GetComponent<BaseEnemyController>().currentHealth -= damage;
            transform.GetChild(1).gameObject.SetActive(true);
            // transform.GetChild(0).gameObject.SetActive(false);
            if (currentThrough <= 0)
            {
                hit = true;
                transform.GetComponent<MeshRenderer>().enabled = false;
            }
            else
            {
                currentThrough -= 1;
                // transform.GetChild(1).gameObject.SetActive(false);
            }
            //  Destroy(gameObject);
        }
        if (other.CompareTag("Club"))
        {
            other.transform.GetComponent<RandomDance>().HP -= damage;
            transform.GetChild(1).gameObject.SetActive(true);
            // transform.GetChild(0).gameObject.SetActive(false);
            if (currentThrough <= 0)
            {
                hit = true;
                transform.GetComponent<MeshRenderer>().enabled = false;
            }
            else
            {
                currentThrough -= 1;
                // transform.GetChild(1).gameObject.SetActive(false);
            }
            //  Destroy(gameObject);
        }*/
        //if (other.CompareTag("Player"))
        //{
        //    other.transform.GetChild(0).GetComponent<CharController>().Health -= damage;
        //    //  Destroy(gameObject);
        //    gameObject.transform.SetParent(BulletPool.transform);
        //    gameObject.SetActive(false);
        //}
    }

    private void BulletHit(Collider other, bool shouldRotate)
    {
        Quaternion rotation = Quaternion.identity;
        if (shouldRotate)
            rotation = Quaternion.LookRotation(transform.position, other.transform.position);

        //  GameObject hit = Instantiate(HitVFX, transform.position, rotation);
        //  Destroy(gameObject);
        if (lifetime < 0)
        {
            gameObject.transform.SetParent(BulletPool.transform);
            gameObject.SetActive(false);
        }
    }

    private void Disable()
    {
        gameObject.transform.SetParent(BulletPool.transform);
        gameObject.SetActive(false);
    }
}
