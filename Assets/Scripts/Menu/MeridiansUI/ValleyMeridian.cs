using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValleyMeridian : MeridianAbstract
{
    public override int hp { get; set; }
    public override int level { get; set; }

    public override void levelUpMeridian()
    {
        level++;
        MeridianController.Instance.SetMeridianLevel(level);
    }
    private void Start()
    {
        propertyData = new Dictionary<string, string>();
        propertyData.Add("Khi huyet", "Valley");
        propertyData.Add("Mana", "Valley");
        propertyData.Add("STBK", "Valley");
        hp = 0;
        level = 0;
    }
}
