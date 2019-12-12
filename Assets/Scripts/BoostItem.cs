using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostItem : MonoBehaviour
{

    public enum Traps
    {
        AreaDamage = 0,
        Explosive = 1
    }
    [Header("Trap General Settings")]
    public Traps TrapType;

    public bool isActive = true;
    public bool affectPlayers = true;
    public bool affectEnemys = false;

    private float damageTimer = 0.0f;
    private List<GameObject> playerGO;
    private List<GameObject> enemiesGO;
    private bool damageOn = false;

    [Header("Area Damage Trap")]
    public float movSpeedFactor = 1.0f;
    public int damage = 10;
    public float damageByNSeconds = 1.0f;

    [Header("VFX")]
    public bool useTargetTransform = false;
    public Transform vfxTransform;
    public Vector3 vfxPosOffset = Vector3.zero;
    public GameObject vfxPrefab;
    private GameObject vfxInstance;

    public float cooldown = 0;
    float time = 0;
    // Use this for initialization
    void Start()
    {
        playerGO = new List<GameObject>();
        enemiesGO = new List<GameObject>();

        damageTimer = damageByNSeconds - 0.1f;

        //if (vfxPrefab)
        //{
        //    vfxInstance = Instantiate(vfxPrefab, transform.position, Quaternion.identity) as GameObject;
        //    vfxInstance.SetActive(false);
        //}

    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if(time <=0)
        {
            GetComponent<SphereCollider>().enabled = true;
            GetComponent<MeshRenderer>().enabled = true;
       
        }
    }


    void ApplyDamage()
    {
        foreach (GameObject p in playerGO)
        {
            if (p != null)
            {
                GameObject fx = Instantiate(vfxPrefab, transform.position, transform.rotation);
                Destroy(fx, 0.5f);
                p.SendMessage("ApplyDamage", damage, SendMessageOptions.DontRequireReceiver);
                damageOn = false;
              
                if (p.GetComponent<PrTopDownCharInventory>().ActualHealth > p.GetComponent<PrTopDownCharInventory>().Health)
                    p.GetComponent<PrTopDownCharInventory>().ActualHealth = p.GetComponent<PrTopDownCharInventory>().Health;
                GetComponent<SphereCollider>().enabled = false;
                GetComponent<MeshRenderer>().enabled = false;
                time = cooldown;

            }

        }
        foreach (GameObject e in enemiesGO)
        {
            if (e != null)
            {
                GameObject fx = Instantiate(vfxPrefab, transform.position, transform.rotation);
                Destroy(fx, 0.5f);
                e.SendMessage("ApplyDamage", damage, SendMessageOptions.DontRequireReceiver);
                damageOn = false;
                GetComponent<SphereCollider>().enabled = false;
                GetComponent<MeshRenderer>().enabled = false;
                time = cooldown;
            }

        }
    }



    void OnTriggerEnter(Collider other)
    {
        if (isActive)
        {
            if (other.gameObject.tag == "Player" && affectPlayers && other.GetComponent<PrTopDownCharInventory>().ActualHealth < other.GetComponent<PrTopDownCharInventory>().Health)
            {
                damageOn = true;
                playerGO.Add(other.gameObject);
                 ApplyDamage();

            }
            if (other.gameObject.tag == "Enemy" && affectEnemys)
            {
                damageOn = true;
                enemiesGO.Add(other.gameObject);
            }
        }

    }

}
