using UnityEngine;

public class TichTaSkill : SkillAbstract
{
    public override void ProcessSkill()
    {
        GameObject o1 = Instantiate(this, MyCharacterController.Instance.transform.position + new Vector3(2f, 0f, 0f), Quaternion.identity).gameObject;
        GameObject o2 = Instantiate(this, MyCharacterController.Instance.transform.position + new Vector3(-2f, 0f, 0f), Quaternion.Euler(0f, 180f, 0f)).gameObject;
        Destroy(o1, skillLifeTime);
        Destroy(o2, skillLifeTime);
    }

    private void Update()
    {
        transform.Translate(2f * Time.deltaTime * new Vector3(2f, 0f, 0f));
    }
}
