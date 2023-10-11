using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillAbstract : MonoBehaviour
{
    public string skillType;
    public int skillDamage;
    public float skillAppearTime, skillLifeTime;
    public abstract void ProcessSkill();

    public void InvokeSkill()
    {
        if (MyCharacterController.Instance != null)
            InvokeRepeating(nameof(ProcessSkill), 0f, skillAppearTime);
        else
            CancelSkill();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        float random = Random.value;
        float rate = MyCharacterController.Instance.critRate / 100f;

        if(skillType.Equals("External"))
        {
            if (random <= rate)
                collision.gameObject.GetComponent<EnemyController>().TakePlayerDamage(skillDamage + MyCharacterController.Instance.externalDamage + MyCharacterController.Instance.critDamage, true);
            else
                collision.gameObject.GetComponent<EnemyController>().TakePlayerDamage(skillDamage + MyCharacterController.Instance.externalDamage, false);
        }else if(skillType.Equals("Internal"))
        {
            if (random <= rate)
                collision.gameObject.GetComponent<EnemyController>().TakePlayerDamage(skillDamage + MyCharacterController.Instance.internalDamage + MyCharacterController.Instance.critDamage, true);
            else
                collision.gameObject.GetComponent<EnemyController>().TakePlayerDamage(skillDamage + MyCharacterController.Instance.internalDamage, false);
        }
            
        MyCharacterController.Instance.HandleInner("Attack");     
    }

    public void CancelSkill()
    {
        CancelInvoke(nameof(ProcessSkill));
    }
}
