using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update

    public abstract int enemyMaxHP { get; set; }
    public abstract int currentEnemyHP { get; set;}
    public abstract int exp { get; set; }
    public abstract int money { get; set; }
    public abstract int qi { get; set; }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            MyCharacterController.Instance.TakeEnemyDamage(20);
            this.GetComponent<Animator>().SetBool("isChasing", false);
        }
        if(collision.gameObject.CompareTag("Skill"))
        {
           
        }
    }

  /*  private void OnCollisionStay2D(Collision2D collision)
    {
        MyCharacterController.Instance.TakeEnemyDamage(20);
    }*/

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {     
            this.GetComponent<Animator>().SetBool("isChasing", true);
        }
    }

    public void TakePlayerDamage(int damage)
    {
        currentEnemyHP -= damage;
        if (currentEnemyHP <= 0)
        {
            if(this.name.Equals("Boss"))
            {
                MapController.Instance.ProcessFinishMap();
            }
            else
            {
                MyCharacterController.Instance.AddKillEnemyChange(this.exp, this.money, this.qi);
            }
            Destroy(gameObject);
        }
    }
}
