using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterController : EnemyController
{
    public float radius;
    public Transform hitRaycast;
    void Start()
    {
        currentEnemyHP = enemyMaxHP;
    }

    public void SetHitPlayer()
    {
        HitPlayer(radius, hitRaycast.position);
    }
}
