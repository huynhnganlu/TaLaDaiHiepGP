using UnityEngine;

public class ThaiCucQuyenSkill : SkillAbstract
{
    private Vector3 dir;
    public override void ProcessSkill()
    {
        GameObject o1 = Instantiate(this, MyCharacterController.Instance.transform.position + new Vector3(2f, 0f, 0f), Quaternion.identity).gameObject;
        Destroy(o1, skillLifeTime);
    }

    private void Start()
    {
        dir = Random.insideUnitCircle.normalized;
    }

    private void Update()
    {
        if (dir != null)
            transform.Translate(2f * Time.deltaTime * dir);
    }
}
