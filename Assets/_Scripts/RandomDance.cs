using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDance : MonoBehaviour
{
    public int dance;
    bool dancestart = false;
    float i;
    public int HP =100;
    bool dead;
    public GameObject DeadVFX;
    public bool notdancer;
    private void OnEnable()
    {
        i = Random.Range(0f, 3.5f);
    }
    private void Update()
    {
        if(HP <=0)
        {
            dead = true;
            if(!notdancer)
              GetComponent<Animator>().SetBool("Dead", dead);
            GameObject VFX = Instantiate(DeadVFX, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), transform.rotation);
            Destroy(VFX,2);
            Destroy(gameObject);
        }
        if (dancestart == false && !notdancer)
        {
            i -= Time.deltaTime;
            if(i <=0)
            {
                dance = Random.Range(1, 8);
                dancestart = true;
                GetComponent<Animator>().SetInteger("Dance", dance);
            }
        }
    }
}
