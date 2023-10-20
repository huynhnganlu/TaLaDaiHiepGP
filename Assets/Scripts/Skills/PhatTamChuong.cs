using UnityEngine;

public class PhatTamChuong : SkillAbstract
{
    public override void ProcessSkill()
    {
        GameObject go = ObjectPoolController.Instance.SpawnObject(gameObject, FindClosestEnemy().position, Quaternion.identity);
        go.GetComponent<Animator>().SetTrigger("Attack");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            float random = Random.value;
            float rate = MyCharacterController.Instance.critRate / 100f;
            int elementalDamage = 0;
            elementalDamage += (int)System.Math.Round(skillDamage / 100f * (10 * MyCharacterController.Instance.elementalYin));
            elementalDamage += (int)System.Math.Round(skillDamage / 100f * (10 * MyCharacterController.Instance.elementalTaichi));

            if (random <= rate)
                collision.gameObject.GetComponent<EnemyController>().TakePlayerDamage(skillDamage + MyCharacterController.Instance.internalDamage + elementalDamage + MyCharacterController.Instance.critDamage, true);
            else
                collision.gameObject.GetComponent<EnemyController>().TakePlayerDamage(skillDamage + MyCharacterController.Instance.internalDamage + elementalDamage, false);
            MyCharacterController.Instance.HandleInner("Attack");
        }
    }
}
