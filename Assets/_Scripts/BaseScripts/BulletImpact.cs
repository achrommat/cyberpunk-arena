using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletImpact : MonoBehaviour
{
    void Update()
    {
        StartCoroutine(Despawn());
    }

    private IEnumerator Despawn()
    {
        yield return new WaitForSeconds(0.5f);
        MF_AutoPool.Despawn(gameObject, 1f);
    }
}
