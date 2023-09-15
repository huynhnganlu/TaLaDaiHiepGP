using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class TichTaSkill : SkillAbstract
{
    public override void InvokeSkill()
    {
        InvokeRepeating("ProcessSkill", 1f, 2f);
    }

    public override void ProcessSkill()
    {
        Instantiate(this, MyCharacterController.Instance.transform.position + new Vector3(2f, 0f, 0f), Quaternion.identity);
        Instantiate(this, MyCharacterController.Instance.transform.position + new Vector3(-2f, 0f, 0f), Quaternion.Euler(0f, 180f, 0f));
    }

    private void Update()
    {
        transform.Translate(2f * Time.deltaTime * new Vector3(2f, 0f, 0f));
        
    }

}
