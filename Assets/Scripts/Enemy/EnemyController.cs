using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update

    public int enemyMaxHP;
    public int currentEnemyHP;
    public int exp;
    public int money;
    public int qi;
    public bool isFlipped = false;



    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            MyCharacterController.Instance.TakeEnemyDamage(20);
        }
       
    }

  /*  private void OnCollisionStay2D(Collision2D collision)
    {
        MyCharacterController.Instance.TakeEnemyDamage(20);
    }*/

    private void OnCollisionExit2D(Collision2D collision)
    {
       
    }

    public void TakePlayerDamage(int damage)
    {
        currentEnemyHP -= damage;

        if((currentEnemyHP <= enemyMaxHP / 2) && name.Equals("Boss"))
        {
            this.GetComponent<Animator>().SetTrigger("isRaging");
        }

        if (currentEnemyHP <= 0)
        {
            if(name.Equals("Boss"))
            {
                //MapController.Instance.ProcessFinishMap();
            }
            else
            {
                MyCharacterController.Instance.AddKillEnemyChange(this.exp, this.money, this.qi);
            }
            this.GetComponent<Animator>().SetTrigger("Death");
            
        }
    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > MyCharacterController.Instance.transform.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < MyCharacterController.Instance.transform.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }
}
