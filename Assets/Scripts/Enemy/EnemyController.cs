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
        if(collision.gameObject.tag == "Player")
        {
            MyCharacterController.Instance.TakeEnemyDamage(20);
        }
        if(collision.gameObject.tag == "Skill")
        {
           
        }
    }

    public void TakePlayerDamage(int damage)
    {
        currentEnemyHP -= damage;
        if (currentEnemyHP <= 0)
        {
            if(this.name.Equals("Boss"))
            {
                MapController.Instance.processFinishMap();
            }
            else
            {
                MyCharacterController.Instance.AddKillEnemyChange(this.exp, this.money, this.qi);
            }
            Destroy(gameObject);
        }
    }
}
