using UnityEngine;

public class HuyetSatDaoSkill : SkillAbstract
{

    public override void ProcessSkill()
    {
        GameObject o1 = ObjectPoolController.Instance.SpawnObject(gameObject, MyCharacterController.Instance.transform.position + new Vector3(3f, 0f, 0f), Quaternion.identity);
        GameObject o2 = ObjectPoolController.Instance.SpawnObject(gameObject, MyCharacterController.Instance.transform.position + new Vector3(-3f, 0f, 0f), Quaternion.Euler(0f, 180f, 0f));
        o1.GetComponent<Animator>().SetTrigger("Attack");
        o2.GetComponent<Animator>().SetTrigger("Attack");
        o1.GetComponent<Animator>().SetFloat("Direction", 1);
        o1.transform.SetParent(MyCharacterController.Instance.transform);
        o2.transform.SetParent(MyCharacterController.Instance.transform);
    }


}
