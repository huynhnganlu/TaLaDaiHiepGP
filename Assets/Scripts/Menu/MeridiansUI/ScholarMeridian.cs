using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScholarMeridian : MeridianAbstract
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
            { "Uy löïc noäi coâng:", internalDamage.ToString()},
            { "Boû qua noäi phoøng:", skipInternalDefense.ToString()},
            { "Noäi coâng baïo kích:", internalCrit.ToString()},
        };   
    }

    public override void LevelUpMeridian()
    {
        level++;
        hp += 1;
        mp += 1;
        internalDamage += 1;
        skipInternalDefense += 1;
        internalCrit += 1;

        UpdatePropertyData();
        GetPropertyData();
        SaveMeridian();
        SaveCharacterData();
    }

    public override void SaveMeridian()
    {
        meridianPrefs.SetInt("scholarlevel", level);
        meridianPrefs.SetInt("scholarhp", hp);
        meridianPrefs.SetInt("scholarmp", mp);
        meridianPrefs.SetInt("scholarinternalDamage", internalDamage);
        meridianPrefs.SetInt("scholarskipInternalDefense", skipInternalDefense);
        meridianPrefs.SetInt("scholarinternalCrit", internalCrit);
        meridianPrefs.Save();
    }

    public override void LoadMeridian()
    {
        level = meridianPrefs.GetInt("scholarlevel");
        hp = meridianPrefs.GetInt("scholarhp");
        mp = meridianPrefs.GetInt("scholarmp");
        internalDamage = meridianPrefs.GetInt("scholarinternalDamage");
        skipInternalDefense = meridianPrefs.GetInt("scholarskipInternalDefense");
        internalCrit = meridianPrefs.GetInt("scholarinternalCrit");
    }

    public override void UpdatePropertyData()
    {
        propertyData["Khí huyeát:"] = hp.ToString();
        propertyData["Noäi löïc:"] = mp.ToString();
        propertyData["Uy löïc noäi coâng:"] = internalDamage.ToString();
        propertyData["Boû qua noäi phoøng:"] = skipInternalDefense.ToString();
        propertyData["Noäi coâng baïo kích:"] = internalCrit.ToString();
    }

    public override void SaveCharacterData()
    {
        characterPrefs.SetInt("hp", characterPrefs.GetInt("hp") + 1 );
        characterPrefs.SetInt("mp", characterPrefs.GetInt("mp") + 1 );
        characterPrefs.SetInt("internalDamage", characterPrefs.GetInt("internalDamage") + 1);
        characterPrefs.SetInt("skipInternalDefense", characterPrefs.GetInt("skipInternalDefense") + 1 );
        characterPrefs.SetInt("internalCrit", characterPrefs.GetInt("internalCrit") + 1 );
        characterPrefs.Save();
    }
}
