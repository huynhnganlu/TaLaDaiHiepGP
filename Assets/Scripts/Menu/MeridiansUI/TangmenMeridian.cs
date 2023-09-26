using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TangmenMeridian : MeridianAbstract
{
   
    private void Start()
    {
        meridianPrefs = MeridianController.Instance.meridianPrefs;
        characterPrefs = MeridianController.Instance.characterPrefs;
        LoadMeridian();
        propertyData = new Dictionary<string, string>
        {
            { "Khí huyeát:", hp.ToString()},
            { "Noäi löïc:", mp.ToString()},
            { "Neù traùnh:", evade.ToString()},
            { "Uy löïc ngoaïi coâng:", externalDamage.ToString()},
            { "Ngoaïi coâng baïo kích:", externalCrit.ToString()},
        };
    }
    public override void LevelUpMeridian()
    {
        level++;
        hp += 1;
        mp += 1;
        evade += 1;
        externalDamage += 1;
        externalCrit += 1;

        UpdatePropertyData();
        GetPropertyData();
        SaveMeridian();
        SaveCharacterData();
    }

    public override void SaveMeridian()
    {
        meridianPrefs.SetInt("tangmenlevel", level);
        meridianPrefs.SetInt("tangmenhp", hp);
        meridianPrefs.SetInt("tangmenmp", mp);
        meridianPrefs.SetInt("tangmenevade", evade);
        meridianPrefs.SetInt("tangmenexternalDamage", externalDamage);
        meridianPrefs.SetInt("tangmenexternalCrit", externalCrit);
        meridianPrefs.Save();
    }

    public override void LoadMeridian()
    {
        level = meridianPrefs.GetInt("tangmenlevel");
        hp = meridianPrefs.GetInt("tangmenhp");
        mp = meridianPrefs.GetInt("tangmenmp");
        evade = meridianPrefs.GetInt("tangmenevade");
        externalDamage = meridianPrefs.GetInt("tangmenexternalDamage");
        externalCrit = meridianPrefs.GetInt("tangmenexternalCrit");
    }

    public override void UpdatePropertyData()
    {
        propertyData["Khí huyeát:"] = hp.ToString();
        propertyData["Noäi löïc:"] = mp.ToString();
        propertyData["Neù traùnh:"] = evade.ToString();
        propertyData["Uy löïc ngoaïi coâng:"] = externalDamage.ToString();
        propertyData["Ngoaïi coâng baïo kích:"] = externalCrit.ToString();
    }

    public override void SaveCharacterData()
    {
        characterPrefs.SetInt("hp", characterPrefs.GetInt("hp") + 1);
        characterPrefs.SetInt("mp", characterPrefs.GetInt("mp") + 1);
        characterPrefs.SetInt("evade", characterPrefs.GetInt("evade") + 1);
        characterPrefs.SetInt("externalDamage", characterPrefs.GetInt("externalDamage") + 1);
        characterPrefs.SetInt("externalCrit", characterPrefs.GetInt("externalCrit") + 1);
        characterPrefs.Save();
    }
}
