using System.Collections;
using System.Collections.Generic;
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
            collision.gameObject.GetComponent<EnemyController>().TakePlayerDamage(skillDamage);
            MyCharacterController.Instance.HandleInner("Attack");
        }
    }
}
