using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LienHoaChuong : SkillAbstract
{
  

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, 0f, 10f * Time.deltaTime * 20f);
    }


    public override void ProcessSkill()
    {
        GameObject o1 = Instantiate(this, MyCharacterController.Instance.transform.position, Quaternion.identity).gameObject;
        o1.transform.SetParent(MyCharacterController.Instance.transform);
        Destroy(o1, skillLifeTime);
    }
}
