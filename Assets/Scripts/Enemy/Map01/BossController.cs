using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : EnemyController
{

    public float radius;
    public Transform hitRaycast;
 
    private void Start()
    {
        currentEnemyHP = enemyMaxHP;
    }

    public void SetHitPlayer()
    {
        HitPlayer(radius, hitRaycast.position);
    }

}
