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
            CancelInvoke(nameof(ProcessSkill));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (gameObject.GetComponent<TuyetTraiSkill>())
            {
                gameObject.transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
            }
            collision.gameObject.GetComponent<EnemyController>().TakePlayerDamage(skillDamage);
            MyCharacterController.Instance.HandleInner("Attack");
        }
    }
}
