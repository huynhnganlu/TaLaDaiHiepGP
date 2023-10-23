using UnityEngine;

public class KimTiDao : SkillAbstract
{
    public override void ProcessSkill()
    {
        GameObject o = ObjectPoolController.Instance.SpawnObject(gameObject, MyCharacterController.Instance.transform.position, Quaternion.Euler(0f, 0f, Random.Range(0, 360)));
        o.GetComponent<Animator>().SetTrigger("Attack");
        o.transform.SetParent(MyCharacterController.Instance.transform);
    }


}
