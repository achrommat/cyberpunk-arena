using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClubZone : MonoBehaviour
{
    CharController player;
    bool playerfind = false;
    public float time = 5;
    public GameObject MainCamera;
    public GameObject PlayerCamera;
     void FixedUpdate()
     {
        if(playerfind)
        {
            if (player.dance == false)
            {
                time -= Time.deltaTime;
            }
            if (player.run < 0.1 && time <= 0)
            {
                player.dance = true;
                PlayerCamera.SetActive(true);
                MainCamera.SetActive(false);
                // player.gameObject.GetComponent<Animator>().applyRootMotion = true;
            }
            if (player.run > 0.1f)
            {


                //  player.gameObject.GetComponent<Animator>().applyRootMotion = false;
                time = 5;
                PlayerCamera.SetActive(false);
                MainCamera.SetActive(true);
                player.dance = false;


            }
        }
     
     }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            player = other.transform.GetChild(0).GetComponent<CharController>();
            playerfind = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerCamera.SetActive(false);
            MainCamera.SetActive(true);
            playerfind = false;
            player.dance = false;
            player = null;
            time = 5;
        }
    }
}
