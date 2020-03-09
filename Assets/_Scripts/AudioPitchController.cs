using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPitchController : MonoBehaviour
{
    [SerializeField] private AudioSource _audio;

    public void Slowdown()
    {
        _audio.pitch = 0.9f;
        StartCoroutine(Reset());
    }

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(1f);
        _audio.pitch = 1f;
    }
}
