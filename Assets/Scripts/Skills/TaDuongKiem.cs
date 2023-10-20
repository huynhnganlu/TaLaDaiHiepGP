using UnityEngine;

public class TaDuongKiem : SkillAbstract
{
    public override void ProcessSkill()
    {
        GameObject o1 = ObjectPoolController.Instance.SpawnObject(gameObject, MyCharacterController.Instance.transform.position + new Vector3(0f, 4f, 0f), Quaternion.identity);
        o1.GetComponent<Animator>().SetTrigger("Attack");
        o1.transform.SetParent(MyCharacterController.Instance.transform);
    }

}
