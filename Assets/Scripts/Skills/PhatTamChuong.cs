using UnityEngine;

public class PhatTamChuong : SkillAbstract
{
    public override void ProcessSkill()
    {
        Instantiate(this, MapController.Instance.GetRandomSpawnPosition(3f, 7f), Quaternion.identity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            float random = Random.value;
            float rate = MyCharacterController.Instance.critRate / 100f;
            if (random <= rate)
                collision.gameObject.GetComponent<EnemyController>().TakePlayerDamage(skillDamage, true);
            else
                collision.gameObject.GetComponent<EnemyController>().TakePlayerDamage(skillDamage, false);
            MyCharacterController.Instance.HandleInner("Attack");
        }
    }
}
