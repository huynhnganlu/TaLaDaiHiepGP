using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeggarMeridian : MeridianAbstract
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
            { "Noäi phoøng:", internalDefense.ToString()},
            { "Uy löïc noäi coâng:", internalDamage.ToString()},
            { "Noäi coâng baïo kích:", internalCrit.ToString()},

        };
    }

    public override void LevelUpMeridian()
    {
        level++;
        hp += 1;
        mp += 1;
        internalDamage += 1;
        internalDefense += 1;
        internalCrit += 1;

        UpdatePropertyData();
        GetPropertyData();
        SaveMeridian();
        SaveCharacterData();
    }

    public override void SaveMeridian()
    {
        meridianPrefs.SetInt("beggarlevel", level);
        meridianPrefs.SetInt("beggarhp", hp);
        meridianPrefs.SetInt("beggarmp", mp);
        meridianPrefs.SetInt("beggarinternalDamage", internalDamage);
        meridianPrefs.SetInt("beggarinternalDefense", internalDefense);
        meridianPrefs.SetInt("beggarinternalCrit", internalCrit);
        meridianPrefs.Save();
    }

    public override void LoadMeridian()
    {
        level = meridianPrefs.GetInt("beggarlevel");
        hp = meridianPrefs.GetInt("beggarhp");
        mp = meridianPrefs.GetInt("beggarmp");
        internalDamage = meridianPrefs.GetInt("beggarinternalDamage");
        internalDefense = meridianPrefs.GetInt("beggarinternalDefense");
        internalCrit = meridianPrefs.GetInt("beggarinternalCrit");
    }

    public override void UpdatePropertyData()
    {
        propertyData["Khí huyeát:"] = hp.ToString();
        propertyData["Noäi löïc:"] = mp.ToString();
        propertyData["Noäi phoøng:"] = internalDefense.ToString();
        propertyData["Uy löïc noäi coâng:"] = internalDamage.ToString();
        propertyData["Noäi coâng baïo kích:"] = internalCrit.ToString();
    }

    public override void SaveCharacterData()
    {
        characterPrefs.SetInt("hp", characterPrefs.GetInt("hp") + 1);
        characterPrefs.SetInt("mp", characterPrefs.GetInt("mp") + 1);
        characterPrefs.SetInt("internalDamage", characterPrefs.GetInt("internalDamage") + 1);
        characterPrefs.SetInt("internalDefense", characterPrefs.GetInt("internalDefense") + 1);
        characterPrefs.SetInt("internalCrit", characterPrefs.GetInt("internalCrit") + 1);
        characterPrefs.Save();
    }
}
