using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoyalMeridian : MeridianAbstract
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
            { "Uy löïc ngoaïi coâng:", externalDamage.ToString()},
            { "Boû qua ngoaïi phoøng:", skipExternalDefense.ToString()},
            { "Ngoaïi coâng baïo kích:", externalCrit.ToString()},
        };
    }

    public override void LevelUpMeridian()
    {
        level++;
        hp += 1;
        mp += 1;
        externalDamage += 1;
        skipExternalDefense += 1;
        externalCrit += 1;

        UpdatePropertyData();
        GetPropertyData();
        SaveMeridian();
        SaveCharacterData();
    }

    public override void SaveMeridian()
    {
        meridianPrefs.SetInt("royallevel", level);
        meridianPrefs.SetInt("royalhp", hp);
        meridianPrefs.SetInt("royalmp", mp);
        meridianPrefs.SetInt("royalexternalDamage", externalDamage);
        meridianPrefs.SetInt("royalskipExternalDefense", skipExternalDefense);
        meridianPrefs.SetInt("royalexternalCrit", externalCrit);
        meridianPrefs.Save();
    }

    public override void LoadMeridian()
    {
        level = meridianPrefs.GetInt("royallevel");
        hp = meridianPrefs.GetInt("royalhp");
        mp = meridianPrefs.GetInt("royalmp");
        externalDamage = meridianPrefs.GetInt("royalexternalDamage");
        skipExternalDefense = meridianPrefs.GetInt("royalskipExternalDefense");
        externalCrit = meridianPrefs.GetInt("royalexternalCrit");
    }

    public override void UpdatePropertyData()
    {
        propertyData["Khí huyeát:"] = hp.ToString();
        propertyData["Noäi löïc:"] = mp.ToString();
        propertyData["Uy löïc ngoaïi coâng:"] = externalDamage.ToString();
        propertyData["Boû qua ngoaïi phoøng:"] = skipExternalDefense.ToString();
        propertyData["Ngoaïi coâng baïo kích:"] = externalCrit.ToString();
    }

    public override void SaveCharacterData()
    {
        characterPrefs.SetInt("hp", characterPrefs.GetInt("hp") + 1);
        characterPrefs.SetInt("mp", characterPrefs.GetInt("mp") + 1 );
        characterPrefs.SetInt("externalDamage", characterPrefs.GetInt("externalDamage") + 1 );
        characterPrefs.SetInt("skipExternalDefense", characterPrefs.GetInt("skipExternalDefense") + 1 );
        characterPrefs.SetInt("externalCrit", characterPrefs.GetInt("externalCrit") + 1 );
        characterPrefs.Save();
    }
}
