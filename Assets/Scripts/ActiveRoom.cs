using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveRoom : MonoBehaviour
{
    public bool room;
    public GameObject EnabledRoom;
    public GameObject FadingRoom;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(FadingRoom !=null)
                FadingRoom.SetActive(false);
            if(EnabledRoom != null)
                EnabledRoom.SetActive(true);
        }
    }
}
