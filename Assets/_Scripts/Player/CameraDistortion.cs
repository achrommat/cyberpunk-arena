using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDistortion : MonoBehaviour
{
    [SerializeField] private GlitchEffect _glitchEffect;
    [SerializeField] private float _intensity;
    [SerializeField] private float _flipIntensity;
    [SerializeField] private float _colorIntensity;
    [SerializeField] private float _period;

    public void PlayDistortion()
    {
        StartCoroutine(Glitch());
    }

    public IEnumerator Glitch()
    {
        _glitchEffect.intensity = _intensity;
        _glitchEffect.flipIntensity = _flipIntensity;
        _glitchEffect.colorIntensity = _colorIntensity;
        yield return new WaitForSeconds(_period);
        _glitchEffect.intensity = 0;
        _glitchEffect.flipIntensity = 0;
        _glitchEffect.colorIntensity = 0;
    }
}
