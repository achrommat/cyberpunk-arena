using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemyController : BaseEnemyController
{

    public Transform[] points;
    private int destPoint = 0;
    private Vector3 resp;

    private void Awake()
    {
        resp = points.Length > 1 ? respawnTarget.transform.position : transform.position;
    }

    private void Patrol()
    {
        if (points.Length == 0)
            return;
        agent.destination = points[destPoint].position;
        destPoint = (destPoint + 1) % points.Length;
    }

    protected override void Update()
    {
        if (!dead)
            if (agent.remainingDistance < 0.5f)
                Patrol();
        DeathHandler();
        anim.SetBool("Dead", dead);
    }

    protected override void Respawn()
    {
        respawnTimer += Time.deltaTime;
        if (respawnTimer > respawnTime)
        {
            Instantiate(respawnVFX, transform.position, transform.rotation);
            GetComponent<ItemDrop>().instantiated = false;
            currentHealth = maxHealth;
            dead = false;
            transform.position = resp;
            respawnTimer = 0.0f;
            agent.isStopped = false;
        }
    }
}
