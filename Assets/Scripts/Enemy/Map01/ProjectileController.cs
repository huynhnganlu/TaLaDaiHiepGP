using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public Vector3 shootDir;
    public int damage;
    // Update is called once per frame
    void Update()
    {
        if (shootDir != null)
        {
            transform.position += 2f * Time.deltaTime * shootDir;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            MyCharacterController.Instance.TakeEnemyDamage(damage);
            ObjectPoolController.Instance.ReturnObjectToPool(gameObject);
        }
    }
}
