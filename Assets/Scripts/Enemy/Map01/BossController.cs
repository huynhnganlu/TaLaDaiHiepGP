using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : EnemyController
{
  

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

    private void Start()
    {
        currentEnemyHP = enemyMaxHP;
    }

    private void Update()
    {
        
    }

   
}
