using UnityEngine;

public class HuyetSatDaoSkill : SkillAbstract
{

    public override void ProcessSkill()
    {
        GameObject o1 = Instantiate(this, MyCharacterController.Instance.transform.position + new Vector3(3f, 0f, 0f), Quaternion.identity).gameObject;
        GameObject o2 = Instantiate(this, MyCharacterController.Instance.transform.position + new Vector3(-3f, 0f, 0f), Quaternion.Euler(0f, 180f, 0f)).gameObject;
        o1.GetComponent<Animator>().SetFloat("Direction", 1);
        o1.transform.SetParent(MyCharacterController.Instance.transform);
        o2.transform.SetParent(MyCharacterController.Instance.transform);
    }


}
