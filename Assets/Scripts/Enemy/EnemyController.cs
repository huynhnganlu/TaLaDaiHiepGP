using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update

    private MyCharacterController player;
    [SerializeField]
    private float enemyMaxHP = 20f;
    private float currentEnemyHP;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<MyCharacterController>();
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
            player.TakeEnemyDamage(20);
        }
        if(collision.gameObject.tag == "Skill")
        {
            TakePlayerDamage(20);
            Debug.Log(currentEnemyHP);
            if (currentEnemyHP <= 0)
                Destroy(gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }

    void TakePlayerDamage(int damage)
    {
        currentEnemyHP -= damage;
    }
}
