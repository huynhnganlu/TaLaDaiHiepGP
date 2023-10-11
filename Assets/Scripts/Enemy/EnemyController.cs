using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update

    public int enemyMaxHP, currentEnemyHP, exp, money, qi, dao, damage;
    private string currentDirection = "left";

    private void OnEnable()
    {
        GetComponent<PolygonCollider2D>().isTrigger = false;
        GetComponent<Animator>().SetBool("Death", false);
    }

    public void TakePlayerDamage(int damage, bool isCrit)
    {
        if (GetComponent<Animator>().GetBool("Death") == false)
        {
            GetComponent<Animator>().Play("Hurt");
            StartCoroutine(ShowPlayerDamage(damage, isCrit));
            currentEnemyHP -= damage;

            if(gameObject.CompareTag("Boss"))
            {
                MapController.Instance.timeSlider.value = currentEnemyHP;
                MapController.Instance.timeText.text = currentEnemyHP.ToString();
                if (currentEnemyHP <= enemyMaxHP / 2){
                    GetComponent<Animator>().SetTrigger("isRaging");
                }
            }     
            if (currentEnemyHP <= 0)
            {
                GetComponent<PolygonCollider2D>().isTrigger = true;
                if(gameObject.CompareTag("Boss"))
                {
                    StartCoroutine(MapController.Instance.ProcessFinishMap());
                }
                else
                {
                    MyCharacterController.Instance.HandleKillEnemy(exp, money, qi);
                }
                GetComponent<Animator>().SetBool("Death", true);
            }
        }
    }

    IEnumerator ShowPlayerDamage(int damage, bool isCrit)
    {
        GameObject dmgText = ObjectPoolController.Instance.SpawnObject(MapController.Instance.damageText, this.transform.position + new Vector3(0f, 1f, 0f), Quaternion.identity);
        dmgText.GetComponent<DameTextController>().SetDamage(damage);
        if (isCrit)
        {
            dmgText.GetComponent<TextMeshPro>().color = Color.yellow;
            dmgText.GetComponent<TextMeshPro>().fontSize = 6;
        }
        else
        {
            dmgText.GetComponent<TextMeshPro>().color = Color.white;
            dmgText.GetComponent<TextMeshPro>().fontSize = 5;
        }
        yield return new WaitForSeconds(1);
        ObjectPoolController.Instance.ReturnObjectToPool(dmgText);
    }

    public void LookAtPlayer()
    {
        if (transform.position.x > MyCharacterController.Instance.transform.position.x && currentDirection.Equals("right"))
        {
            currentDirection = "left";
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        else if (transform.position.x < MyCharacterController.Instance.transform.position.x && currentDirection.Equals("left"))
        {
            currentDirection = "right";
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            MyCharacterController.Instance.TakeEnemyDamage(damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.GetChild(0).position, 6f);
    }
}
