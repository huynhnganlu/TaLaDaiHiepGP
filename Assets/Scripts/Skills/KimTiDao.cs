using UnityEngine;

public class KimTiDao : SkillAbstract
{
    public override void ProcessSkill()
    {
        GameObject o = Instantiate(this, MyCharacterController.Instance.transform.position, Quaternion.Euler(0f, 0f, Random.Range(0, 360))).gameObject;
        o.transform.SetParent(MyCharacterController.Instance.transform);
    }
}
