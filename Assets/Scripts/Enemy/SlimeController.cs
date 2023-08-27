using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : EnemyController
{
    public override float enemyMaxHP { get; set; }
    public override float currentEnemyHP { get; set; }

    public void Start()
    {
        enemyMaxHP = 20f;
        currentEnemyHP = enemyMaxHP;
    }



}
