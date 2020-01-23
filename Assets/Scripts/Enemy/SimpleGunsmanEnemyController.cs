using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class SimpleGunsmanEnemyController : BaseEnemyController
{
    protected override IEnumerator Attack()
    {
        agent.isStopped = true;
        yield return new WaitForSeconds(.5f);
        audioSource.PlayOneShot(shotClip);
        shootPos.transform.localRotation = Quaternion.Euler(Random.Range(-recoil, recoil), Random.Range(-recoil, recoil), 0);
        CreateBullet();
        agent.isStopped = false;

        /*if (multishot == true)
        {
            audioSource.PlayOneShot(shotClip);
            int[] pool = { -5, -2, 0, 2, 5 };
            for (int i = 0; i < 5; i++)
            {
                shootPos.transform.localRotation = Quaternion.Euler(Random.Range(-recoil, recoil), Random.Range(-recoil, recoil) + pool[i], 0);
                CreateBullet();
                shootPos.transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
        }
        else
        {
            audioSource.PlayOneShot(shotClip);
            shootPos.transform.localRotation = Quaternion.Euler(Random.Range(-recoil, recoil), Random.Range(-recoil, recoil), 0);
            CreateBullet();
        }*/
    }    
}
