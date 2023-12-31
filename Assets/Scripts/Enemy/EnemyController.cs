using System.Collections;
using TMPro;
using UnityEngine;

public abstract class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update

    public int enemyMaxHP, currentEnemyHP, exp, money, qi, dao, damage, shop;
    private string currentDirection = "left";
    [SerializeField]
    private GameObject projectile;

    private void OnEnable()
    {
        currentEnemyHP = enemyMaxHP;
        GetComponent<PolygonCollider2D>().isTrigger = false;
        GetComponent<Animator>().SetBool("Death", false);
    }

    private void Awake()
    {
        if (MapController.Instance.characterPrefs.GetInt("mapdiff") == 1)
        {
            enemyMaxHP = (int)System.Math.Round(enemyMaxHP * 1.5f);
            money *= (int)System.Math.Round(money * 1.5f);
            qi *= (int)System.Math.Round(qi * 1.5f);
            dao *= (int)System.Math.Round(dao * 1.5f);
            damage *= (int)System.Math.Round(damage * 1.5f);
            shop *= (int)System.Math.Round(shop * 1.5f);
        }
    }
    public void TakePlayerDamage(int damage, bool isCrit)
    {
        if (GetComponent<Animator>().GetBool("Death") == false)
        {
            GetComponent<Animator>().Play("Hurt");
            StartCoroutine(ShowPlayerDamage(damage, isCrit));
            currentEnemyHP -= damage;

            if (gameObject.CompareTag("Boss"))
            {
                MapController.Instance.timeSlider.value = currentEnemyHP;
                MapController.Instance.timeText.text = currentEnemyHP.ToString();
                if (currentEnemyHP <= enemyMaxHP / 2)
                {
                    GetComponent<Animator>().SetTrigger("isRaging");
                }
            }
            if (currentEnemyHP <= 0)
            {
                AudioManager.Instance.PlaySE("EnemyDeathSE");
                GetComponent<PolygonCollider2D>().isTrigger = true;
                if (gameObject.CompareTag("Boss"))
                    StartCoroutine(MapController.Instance.ProcessFinishMap());
                MyCharacterController.Instance.HandleKillEnemy(exp, money, qi, dao, shop);
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

    public void HitPlayer(float radius, Vector3 hitPos)
    {
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(hitPos, radius, LayerMask.GetMask("Player")))
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
            MyCharacterController.Instance.TakeEnemyDamage(20);
        }
    }

    public void ShootProjectile()
    {
        if (projectile != null)
        {
            if (gameObject.name.Equals("Boss03"))
            {              
                if(gameObject.GetComponent<Animator>().GetBool("isRaging") == true)
                {
                    GameObject clone1 = ObjectPoolController.Instance.SpawnObject(projectile, transform.position + new Vector3(-1f, 2f, 0f), Quaternion.identity);
                    clone1.GetComponent<ProjectileController>().shootDir = (MyCharacterController.Instance.transform.position - transform.position - new Vector3(-1f, 2f, 0f)).normalized;
                    clone1.GetComponent<ProjectileController>().damage = this.damage;
                    GameObject clone2 = ObjectPoolController.Instance.SpawnObject(projectile, transform.position + new Vector3(-1f, -2f, 0f), Quaternion.identity);
                    clone2.GetComponent<ProjectileController>().shootDir = (MyCharacterController.Instance.transform.position - transform.position - new Vector3(-1f, -2f, 0f)).normalized;
                    clone2.GetComponent<ProjectileController>().damage = this.damage;
                }
                else
                {
                    GameObject clone = ObjectPoolController.Instance.SpawnObject(projectile, transform.position + new Vector3(-1f, 0f, 0f), Quaternion.identity);
                    clone.GetComponent<ProjectileController>().shootDir = (MyCharacterController.Instance.transform.position - transform.position - new Vector3(-1f, 0f, 0f)).normalized;
                    clone.GetComponent<ProjectileController>().damage = this.damage;
                }
            }
            else
            {
                GameObject clone = ObjectPoolController.Instance.SpawnObject(projectile, transform.position + new Vector3(-1f, 0f, 0f), Quaternion.identity);
                clone.GetComponent<ProjectileController>().shootDir = (MyCharacterController.Instance.transform.position - transform.position - new Vector3(-1f, 0f, 0f)).normalized;
                clone.GetComponent<ProjectileController>().damage = this.damage;
            }
        }
    }

}
