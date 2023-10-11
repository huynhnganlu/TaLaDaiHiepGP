using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuCotChuong : SkillAbstract
{
    private float delay = 2f, elapse = 0f;
    public override void ProcessSkill()
    {
        GameObject o1 = Instantiate(this, MyCharacterController.Instance.transform.position, Quaternion.identity).gameObject;
        Destroy(o1, skillLifeTime);      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        elapse += Time.deltaTime;
        if (elapse >= delay)
        {
            float random = Random.value;
            float rate = MyCharacterController.Instance.critRate / 100f;
            if(random <= rate)
                collision.gameObject.GetComponent<EnemyController>().TakePlayerDamage(skillDamage, true);
            else
                collision.gameObject.GetComponent<EnemyController>().TakePlayerDamage(skillDamage, false);
            MyCharacterController.Instance.HandleInner("Attack");                
            elapse = 0f;
        }          
    }

  
}
