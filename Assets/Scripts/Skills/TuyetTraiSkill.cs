using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;

public class TuyetTraiSkill : SkillAbstract
{
    public override void ProcessSkill()
    {
        GameObject o1 = Instantiate(this, MyCharacterController.Instance.transform.position , Quaternion.Euler(0f, 0f, Random.Range(0f, 360f))).gameObject;
        Destroy(o1, skillLifeTime);
    }

    private void Update()
    {
        transform.Translate(2f * Time.deltaTime * new Vector3(1f, -1f, 0f));

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {            
            gameObject.transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
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
