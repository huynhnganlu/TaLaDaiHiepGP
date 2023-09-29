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
    public int damage;
    public bool isFlipped = false;

    public void TakePlayerDamage(int damage)
    {
        if (GetComponent<Animator>().GetBool("Death") == false)
        {
            GetComponent<Animator>().Play("Hurt");
            currentEnemyHP -= damage;

            if ((currentEnemyHP <= enemyMaxHP / 2) && name.Equals("Boss"))
            {
                GetComponent<Animator>().SetTrigger("isRaging");
            }

            if (currentEnemyHP <= 0)
            {
                GetComponent<PolygonCollider2D>().isTrigger = true;
                if (name.Equals("Boss"))
                {
                    //MapController.Instance.ProcessFinishMap();
                }
                else
                {
                    MyCharacterController.Instance.AddKillEnemyChange(this.exp, this.money, this.qi);
                }
                GetComponent<Animator>().SetBool("Death", true);

            }
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

    public void HitPlayer(float radius,Vector3 hitPos)
    {
        foreach(Collider2D collider in Physics2D.OverlapCircleAll(hitPos, radius, LayerMask.GetMask("Player")))
        {
            if (collider.CompareTag("Player"))
            {
                MyCharacterController.Instance.TakeEnemyDamage(damage);
            }
        }
    }
}
