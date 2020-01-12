using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetAim : MonoBehaviour
{
    bool HaveTarget;
    //  public List<Transform> target = new List<Transform>();
    public GameObject body;
    private Quaternion shootrot;
    private Quaternion playeryrot;

    public Transform Shootpos;
    public GameObject target;

    public float range = 250;
    public List<GameObject> Enemies;
    public bool AimHelper = false;
    public bool AutoAim = false;
    
    private void Awake()
    {
        shootrot = Shootpos.transform.localRotation;
        playeryrot = transform.parent.localRotation;
    }




    void FixedUpdate()
    {
        if (Enemies.Count > 0 && transform.parent.GetComponent<CharController>().aiming)
        {
            float distance = range;
            GameObject closestGo = null;
            foreach (GameObject go in Enemies)
            {
                if (go == null)
                {
                    target = null;
                    Enemies.Remove(go);
                }
                else
                {
                    float enemyHealth = go.GetComponent<CharController>() ? go.GetComponent<CharController>().Health : go.GetComponent<BaseEnemyController>().currentHealth;
                    if (enemyHealth <= 0)
                    {
                        target = null;
                        Enemies.Remove(go);
                    }
                    else
                    {
                        Vector3 diff = go.transform.position - transform.position;
                        float curDistance = diff.sqrMagnitude;
                        if (curDistance < distance)
                        {
                            closestGo = go;
                            distance = curDistance;
                        }
                        target = closestGo;
                    }
                }
                if (AimHelper || AutoAim)
                {
                    //transform.GetChild(0).LookAt(new Vector3(target.transform.position.x, target.transform.position.y +1, target.transform.position.z));
                    if(target.transform.GetChild(2).position!=null)
                    Shootpos.transform.LookAt(target.transform.GetChild(2).position);

                }
                if (AutoAim)
                {
                    //transform.GetChild(0).LookAt(new Vector3(target.transform.position.x, target.transform.position.y +1, target.transform.position.z));
                    transform.parent.LookAt(new Vector3(target.transform.position.x, transform.parent.parent.position.y, target.transform.position.z));

                }
            }
        }
        else
        {
            if (AimHelper)
            {
                target = null;
                // transform.GetChild(0).localRotation = Quaternion.Euler(0, 0, 0);
                Shootpos.transform.localRotation = shootrot;
            }
            if (AutoAim)
            {
                target = null;
                transform.parent.LookAt(transform.parent.position + transform.parent.GetComponent<CharController>().movement * Time.deltaTime);
            }
        }

    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemies.Add(other.gameObject);
            HaveTarget = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemies.Remove(other.gameObject);
            HaveTarget = false;

        }
    }
}
