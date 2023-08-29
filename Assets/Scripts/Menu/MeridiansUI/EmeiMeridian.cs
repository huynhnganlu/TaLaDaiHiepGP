using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EmeiMeridian : MeridianAbstract
{
    public override int hp { get; set; }
    public override int level { get; set; }

    public override void levelUpMeridian()
    {
        hp += 10;
        level++;
        characterData.hp += hp;
        characterData.qi -= 10;
        MeridianController.Instance.qiHolder.text = characterData.qi.ToString();
        MeridianController.Instance.SetMeridianLevel(level);
    }

    private void Start()
    {
        propertyData = new Dictionary<string, string>();
        propertyData.Add("Khi huyet", "Emei");
        propertyData.Add("Mana", "Emei");
        hp = 0;
        level = 0;
        GetPropertyData();
    }

   

}
