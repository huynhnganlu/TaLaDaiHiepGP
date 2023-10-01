using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanhNguyetDao : SkillAbstract
{
    public override void ProcessSkill()
    {
        GameObject o1 = Instantiate(this, MyCharacterController.Instance.transform.position + new Vector3(0f, 2f, 0f), Quaternion.identity).gameObject;
        Destroy(o1, skillLifeTime);
    }

    private void Update()
    {
        transform.Rotate(0f, 0f, 30f * Time.deltaTime * 20f);
        transform.Translate(0f, -0.03f, 0f, Space.World);

    }
}
