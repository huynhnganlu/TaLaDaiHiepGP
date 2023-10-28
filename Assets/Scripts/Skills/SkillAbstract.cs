using System.Collections;
using UnityEngine;

public abstract class SkillAbstract : MonoBehaviour
{
    public string skillType, skillElemental, skillName;
    public int skillDamage, skillAppearTime, skillLifeTime;
    public abstract void ProcessSkill();

    public void InvokeSkill()
    {
        InvokeRepeating(nameof(ProcessSkill), 0f, skillAppearTime);
    }

 
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        float random = Random.value;
        float rate = MyCharacterController.Instance.critRate / 100f;
        int elementalDamage = 0;
        int totalSkillDamage = 0;

        if (MyCharacterController.Instance.damageDictionary.ContainsKey(skillName)){ 
            int level = MyCharacterController.Instance.levelDictionary[MyCharacterController.Instance.damageDictionary[skillName]];
            if (level == 1)
                totalSkillDamage = skillDamage;
            else
                totalSkillDamage = skillDamage + (int)System.Math.Round(skillDamage * (level * 0.2));
        }

        switch (skillElemental)
        {
            case "yang":
                elementalDamage += (int)System.Math.Round(totalSkillDamage / 100f * (10 * MyCharacterController.Instance.elementalYang));
                elementalDamage += (int)System.Math.Round(totalSkillDamage / 100f * (10 * MyCharacterController.Instance.elementalTaichi));
                break;
            case "yin":
                elementalDamage += (int)System.Math.Round(totalSkillDamage / 100f * (10 * MyCharacterController.Instance.elementalYin));
                elementalDamage += (int)System.Math.Round(totalSkillDamage / 100f * (10 * MyCharacterController.Instance.elementalTaichi));
                break;
            case "taichi":
                elementalDamage += (int)System.Math.Round(totalSkillDamage / 100f * (10 * MyCharacterController.Instance.elementalTaichi));
                break;
        }

        if (skillType.Equals("External"))
        {
            if (random <= rate)
                collision.gameObject.GetComponent<EnemyController>().TakePlayerDamage((int)(System.Math.Round(totalSkillDamage * 1.5)) + MyCharacterController.Instance.externalDamage + MyCharacterController.Instance.critDamage + elementalDamage, true);
            else
                collision.gameObject.GetComponent<EnemyController>().TakePlayerDamage(totalSkillDamage + MyCharacterController.Instance.externalDamage + elementalDamage, false);
        }
        else if (skillType.Equals("Internal"))
        {
            if (random <= rate)
                collision.gameObject.GetComponent<EnemyController>().TakePlayerDamage((int)(System.Math.Round(totalSkillDamage * 1.5)) + totalSkillDamage + MyCharacterController.Instance.internalDamage + MyCharacterController.Instance.critDamage + elementalDamage, true);
            else
                collision.gameObject.GetComponent<EnemyController>().TakePlayerDamage(totalSkillDamage + MyCharacterController.Instance.internalDamage + elementalDamage, false);
        }

        MyCharacterController.Instance.HandleInner("Attack");
    }

    public void CancelSkill()
    {
        CancelInvoke(nameof(ProcessSkill));
    }

    public Transform FindClosestEnemy()
    {
        float distanceClosest = Mathf.Infinity;
        Transform cloestEnemy = null;
        GameObject objectHolder = GameObject.Find("MapPrefab/ObjectParentHolder");
        foreach(Transform child in objectHolder.transform){
            if(child != null && child.gameObject.activeSelf && (child.CompareTag("Enemy") || child.CompareTag("Boss")))
            {
                float distance = (child.position - MyCharacterController.Instance.transform.position).sqrMagnitude;
                if(distance < distanceClosest && distance <= 200f)
                {
                    distanceClosest = distance;
                    cloestEnemy = child;
                }
            }
        }
        return cloestEnemy;
    }

}
