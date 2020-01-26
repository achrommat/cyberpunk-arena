using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    bool death;
    public bool instantiated = false;
    public ItemPool loot;
    public float distance =1;
    public float dropChance =100; //100 = 100%
   

    protected void OnEnable()
    {
        instantiated = false;
    }
    public void Update()
    {
        if(!instantiated)
        {
           // death = transform.GetComponent<BaseEnemyController>().dead;
            if (death )
            {
                float drop = Random.Range(0, 100);

                if(drop <= dropChance)
                {
                    for (int i = 0; i < 1; i++)
                    {
                        int lootLevel = Random.Range(0, loot.itemDropFx.Count - 1);
                        GameObject item = Instantiate(loot.itemDropFx[lootLevel], new Vector3(transform.position.x + Random.Range(-distance, distance), transform.position.y, transform.position.z + Random.Range(-distance, distance)), Quaternion.Euler(0, Random.Range(0, 380), 90));
                        item.transform.GetChild(0).GetComponent<LootSpawn>().lootLevel = lootLevel;
                        instantiated = true;
                    }
                }
                else
                {
                    instantiated = true;
                }
            }
        }
    }
}
