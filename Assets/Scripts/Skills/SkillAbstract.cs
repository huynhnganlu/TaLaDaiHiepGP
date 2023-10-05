using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillAbstract : MonoBehaviour
{
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
            collision.gameObject.GetComponent<EnemyController>().TakePlayerDamage(skillDamage);
            MyCharacterController.Instance.HandleInner("Attack");     
    }

    public void CancelSkill()
    {
        CancelInvoke(nameof(ProcessSkill));
    }
}
