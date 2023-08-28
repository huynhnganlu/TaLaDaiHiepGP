using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : EnemyController
{
    public override int enemyMaxHP { get; set; }
    public override int currentEnemyHP { get; set; }
    public override int exp { get; set; }
    public override int money { get; set; }
    public override int qi { get; set; }

    public static BossController Instance;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }


}
