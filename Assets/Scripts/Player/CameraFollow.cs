using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public float distance;
    public float distance2;
    
    private void Update()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y +distance, player.transform.position.z + distance2);
    }
}
