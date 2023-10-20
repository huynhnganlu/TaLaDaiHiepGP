using System.Threading.Tasks;
using UnityEngine;

public class HuCotChuong : SkillAbstract
{
    private float delay = 2f, elapse = 0f;
    public async override void ProcessSkill()
    {
        if (FindClosestEnemy() != null)
        {
            GameObject go = ObjectPoolController.Instance.SpawnObject(gameObject, FindClosestEnemy().position, Quaternion.identity);
            await Task.Delay(skillLifeTime * 1000);
            ObjectPoolController.Instance.ReturnObjectToPool(go);
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        elapse += Time.deltaTime;
        if (elapse >= delay)
        {
            float random = Random.value;
            float rate = MyCharacterController.Instance.critRate / 100f;
            int elementalDamage = 0;
            elementalDamage += (int)System.Math.Round(skillDamage / 100f * (10 * MyCharacterController.Instance.elementalYang));
            elementalDamage += (int)System.Math.Round(skillDamage / 100f * (10 * MyCharacterController.Instance.elementalTaichi));

            if (random <= rate)
                collision.gameObject.GetComponent<EnemyController>().TakePlayerDamage(skillDamage + MyCharacterController.Instance.externalDamage + MyCharacterController.Instance.critDamage + elementalDamage, true);
            else
                collision.gameObject.GetComponent<EnemyController>().TakePlayerDamage(skillDamage + MyCharacterController.Instance.externalDamage + elementalDamage, false);
            MyCharacterController.Instance.HandleInner("Attack");
            elapse = 0f;
        }
    }


}
