using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemyController : BaseEnemyController
{

    public Transform[] points;
    private int destPoint = 0;

    private void Patrol()
    {
        if (points.Length == 0)
            return;
        agent.destination = points[destPoint].position;
        destPoint = (destPoint + 1) % points.Length;
    }

    public override void Update()
    {
        DeathHandler();
        if (agent.remainingDistance < 0.5f)
            Patrol();
    }

    public override void Respawn()
    {
        currentHealth = maxHealth;
        dead = false;
        Vector3 resp = RespawnTarget.transform.position;
        transform.position = resp;
    }
}
