using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public GameObject[] place;

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Items"))
        {
            if(place[other.GetComponent<ItemController>().place].transform.childCount > 0)
            {
                Destroy(place[other.GetComponent<ItemController>().place].transform.GetChild(0).gameObject);
            }
            GameObject item = Instantiate(other.gameObject);
            item.transform.SetParent(place[other.GetComponent<ItemController>().place].transform);
            item.transform.localPosition = item.GetComponent<ItemController>().Axis;
            item.transform.localRotation = Quaternion.Euler(item.GetComponent<ItemController>().Rotation);
            Destroy(other.gameObject);
        }
    }
}
