using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;

public class TuyetTraiSkill : SkillAbstract
{
    public override void ProcessSkill()
    {
        GameObject o1 = Instantiate(this, MyCharacterController.Instance.transform.position , Quaternion.Euler(0f, 0f, Random.Range(0f, 360f))).gameObject;
    }

    private void Update()
    {
        transform.Translate(2f * Time.deltaTime * new Vector3(1f, -1f, 0f));

    }
}
