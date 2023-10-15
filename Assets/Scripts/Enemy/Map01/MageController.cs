using System.Collections;
using UnityEngine;

public class MageController : EnemyController
{
    [SerializeField]
    private GameObject projectile;

    void Start()
    {
        currentEnemyHP = enemyMaxHP;
    }

    public IEnumerator ShootProjectile(Vector3 dir)
    {
        yield return new WaitForSeconds(0.9f);
        GameObject clone = ObjectPoolController.Instance.SpawnObject(projectile, transform.position + new Vector3(-1f, 0f, 0f), Quaternion.identity);
        clone.GetComponent<ProjectileController>().shootDir = (dir - transform.position - new Vector3(-1f, 0f, 0f)).normalized;
        clone.GetComponent<ProjectileController>().damage = this.damage;
    }

    public void StartShoot(Vector3 dir)
    {
        StartCoroutine(ShootProjectile(dir));
    }
}
