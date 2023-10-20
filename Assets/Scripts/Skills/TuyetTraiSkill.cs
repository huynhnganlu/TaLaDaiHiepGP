using System.Threading.Tasks;
using UnityEngine;

public class TuyetTraiSkill : SkillAbstract
{
    public async override void ProcessSkill()
    {
        GameObject o1 = ObjectPoolController.Instance.SpawnObject(gameObject, MyCharacterController.Instance.transform.position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
        await Task.Delay(skillLifeTime * 1000);
        ObjectPoolController.Instance.ReturnObjectToPool(o1);
    }

    private void Update()
    {
        transform.Translate(3f * Time.deltaTime * new Vector3(1f, -1f, 0f));

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            gameObject.transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
            float random = Random.value;
            float rate = MyCharacterController.Instance.critRate / 100f;
            int elementalDamage = 0;
            elementalDamage += (int)System.Math.Round(skillDamage / 100f * (10 * MyCharacterController.Instance.elementalTaichi));
            if (random <= rate)
                collision.gameObject.GetComponent<EnemyController>().TakePlayerDamage(skillDamage + elementalDamage + MyCharacterController.Instance.critDamage, true);
            else
                collision.gameObject.GetComponent<EnemyController>().TakePlayerDamage(skillDamage + elementalDamage, false);
            MyCharacterController.Instance.HandleInner("Attack");
        }
    }
}
