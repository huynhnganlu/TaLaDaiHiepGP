using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public Vector3 shootDir;
    public int damage;
    public float speed;

    void Update()
    {
        if (shootDir != null)
        {
            transform.position += speed * Time.deltaTime * shootDir;           
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
