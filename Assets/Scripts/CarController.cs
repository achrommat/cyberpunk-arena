using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public GameObject VFX;
    public void AnimationStart()
    {
        VFX.SetActive(true);
    }
    public void AnimationEnd()
    {
        VFX.SetActive(false);
    }
}
