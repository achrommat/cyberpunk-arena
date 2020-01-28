using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSpawn : MonoBehaviour
{
    public int lootLevel;
    public int lootID;
    public GameObject loot;
    public void SpawnLootObject()
    {
        GameObject lootobj = Instantiate(loot, transform.position, Quaternion.Euler(Vector3.zero));
        lootobj.GetComponent<LootController>().objLevel = lootLevel;
        lootobj.GetComponent<LootController>().objID = lootID;
        Destroy(transform.gameObject);
    }
}
