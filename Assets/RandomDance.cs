using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDance : MonoBehaviour
{
    public int dance;
    bool dancestart = false;
    float i;
    private void Awake()
    {
       i = Random.Range(0f, 3.5f);
    }

    private void Update()
    {
        if(dancestart == false)
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
