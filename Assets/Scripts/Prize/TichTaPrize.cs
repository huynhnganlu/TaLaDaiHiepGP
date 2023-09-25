using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TichTaPrize : PrizeAbstract
{
    public SkillAbstract skill;
    public override void ProcessPrize()
    {
        MapController.Instance.OnSkillListChange(skill);
    }

  
}
