using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootController : MonoBehaviour
{
    public int objLevel; // от 0 до 4
    public int objID; // от 0 до Х
    public GameObject lootObj;
    public GameObject rayObj;
    public ItemPool itemPool;

    public void Start()
    {
        objID = Random.Range(0, itemPool.itemPool.Count);
        GameObject item = Instantiate(itemPool.itemPool[objID], transform.position, transform.rotation);
        item.transform.parent = lootObj.transform;
       // item.transform.localPosition = new Vector3(0, 0, 0);
       // item.transform.localRotation = Quaternion.Euler(0, 0, 0);
     //   objLevel = Random.Range(0, 4);
        GameObject field = Instantiate(itemPool.itemField[objLevel], transform.position, transform.rotation);
        field.transform.parent = lootObj.transform;
        field.transform.localPosition = new Vector3(0, 0, 0);
        field.transform.localRotation = Quaternion.Euler(0, 0, -90);
        //GameObject item = Instantiate(itemPool.itemPool[Random.Range(0, itemPool.itemPool.Count - 1)], transform.position, transform.rotation);
        //item.transform.parent = lootObj.transform;
        //item.transform.localPosition = new Vector3(0, 0, 0);
        //item.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }
}
