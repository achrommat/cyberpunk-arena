using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public GameObject bullet;
    public GameObject ShootPos;
    public GameObject ShootFX;
    public float speed;
    public bool shooting;
    public void Update()
    {
        if(shooting == true)
        {
            GameObject newbullet = Instantiate(bullet, transform.position, transform.rotation);
            Destroy(newbullet, 2);
        }
    }
}
