using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickFeedBack : MonoBehaviour
{
    public Transform Handle;

    public void Update()
    {
        transform.position = Handle.position;
    }
}
