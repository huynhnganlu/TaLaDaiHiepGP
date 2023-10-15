using UnityEngine;

public class TaDuongKiem : SkillAbstract
{
    public override void ProcessSkill()
    {
        GameObject o1 = Instantiate(this, MyCharacterController.Instance.transform.position + new Vector3(0f, 4f, 0f), Quaternion.identity).gameObject;
        o1.transform.SetParent(MyCharacterController.Instance.transform);
    }

}
