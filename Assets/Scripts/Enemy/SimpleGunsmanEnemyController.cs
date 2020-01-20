using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class SimpleGunsmanEnemyController : BaseEnemyController
{
    public override IEnumerator Attack()
    {
        // реализуется в наследуемых скриптах
        agent.isStopped = true;
        yield return new WaitForSeconds(.5f);
        if (multishot == true)
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
        }
        agent.isStopped = false;
    }

    private void CreateBullet()
    {
        GameObject newbullet = bulletPool.transform.GetChild(0).gameObject;
        newbullet.GetComponent<BaseEnemyBulletController>().damage = this.damage;
        newbullet.transform.position = shootPos.transform.position;
        newbullet.transform.rotation = shootPos.transform.rotation;
        newbullet.transform.SetParent(null);
        newbullet.SetActive(true);
    }
}
