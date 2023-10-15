using UnityEngine;

public class ThaiCucKiemSkill : SkillAbstract
{
    public override void ProcessSkill()
    {
        Instantiate(this, MyCharacterController.Instance.transform.position + new Vector3(0f, -2f, 0f), Quaternion.Euler(0f, 0f, 53f));
        Instantiate(this, MyCharacterController.Instance.transform.position + new Vector3(-2f, 0f, 0f), Quaternion.Euler(0f, 0f, -37f));
        Instantiate(this, MyCharacterController.Instance.transform.position + new Vector3(2f, 0f, 0f), Quaternion.Euler(0f, 0f, 143f));
        Instantiate(this, MyCharacterController.Instance.transform.position + new Vector3(0f, 2f, 0f), Quaternion.Euler(0f, 0f, -127f));


    }

    private void Update()
    {
        transform.Translate(2f * Time.deltaTime * new Vector3(-1.2f, -1f, 0f));
    }

}
