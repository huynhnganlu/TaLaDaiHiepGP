using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : EnemyController
{
    public override int enemyMaxHP { get; set; }
    public override int currentEnemyHP { get; set; }
    public override int exp { get; set; }
    public override int money { get; set; }
    public override int qi { get; set; }

    public void Start()
    {
        enemyMaxHP = 20;
        exp = 10;
        money = 10;
        qi = 20;
        currentEnemyHP = enemyMaxHP;
    }



}
