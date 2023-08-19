using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update

    private float enemyMaxHP = 20f;
    private float currentEnemyHP;
    
    

    void Start()
    {
        currentEnemyHP = enemyMaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            MyCharacterController.Instance.TakeEnemyDamage(20);
        }
        if(collision.gameObject.tag == "Skill")
        {
           
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }

    public void TakePlayerDamage(int damage)
    {
        Debug.Log(currentEnemyHP);
        currentEnemyHP -= damage;
        if (currentEnemyHP <= 0)
        {
            MyCharacterController.Instance.AddExp(100);
            Destroy(gameObject);

        }
    }
}
