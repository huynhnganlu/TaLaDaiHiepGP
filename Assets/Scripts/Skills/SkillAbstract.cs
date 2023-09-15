using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillAbstract : MonoBehaviour
{
    public abstract void ProcessSkill();
    public abstract void InvokeSkill();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyController>().TakePlayerDamage(20);
            Destroy(gameObject);
        }
    }
}
