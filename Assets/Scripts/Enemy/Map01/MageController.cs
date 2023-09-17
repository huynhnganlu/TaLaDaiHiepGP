using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageController : EnemyController
{
    [SerializeField]
    private GameObject projectile;

    void Start()
    {
        currentEnemyHP = enemyMaxHP;
    }

    void Update()
    {
       
    }

    public IEnumerator ShootProjectile(Vector3 dir)
    {
        yield return new WaitForSeconds(0.9f);
        GameObject clone = Instantiate(projectile, transform.position + new Vector3(-1f, 0f, 0f), Quaternion.identity);
        clone.GetComponent<ProjectileController>().shootDir = (dir - transform.position - new Vector3(-1f, 0f, 0f)).normalized;
    }

    public void StartShoot(Vector3 dir)
    {
        StartCoroutine(ShootProjectile(dir));
    }
}
