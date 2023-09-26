using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaolinMeridian : MeridianAbstract
{
   

    private void Start()
    {
        meridianPrefs = MeridianController.Instance.meridianPrefs;
        characterPrefs = MeridianController.Instance.characterPrefs;
        LoadMeridian();
        propertyData = new Dictionary<string, string>
        {
            { "Khí huyeát:", hp.ToString()},
            { "Noäi löïc:", level.ToString()},
            { "Ngoaïi phoøng:", externalDefense.ToString()},
            { "Hoài khí huyeát:", hpRegen.ToString()},
            { "Boû qua ngoaïi phoøng:", skipExternalDefense.ToString()},
        };
    }

    public override void LevelUpMeridian()
    {
        level++;
        hp += 1;
        mp += 1;
        externalDefense += 1;
        hpRegen += 1;
        skipExternalDefense += 1;

        UpdatePropertyData();
        GetPropertyData();
        SaveMeridian();
        SaveCharacterData();
    }

    public override void SaveMeridian()
    {
        meridianPrefs.SetInt("shaolinlevel", level);
        meridianPrefs.SetInt("shaolinhp", hp);
        meridianPrefs.SetInt("shaolinmp", mp);
        meridianPrefs.SetInt("shaolinexternalDefense", externalDefense);
        meridianPrefs.SetInt("shaolinhpRegen", hpRegen);
        meridianPrefs.SetInt("shaolinskipExternalDefense", skipExternalDefense);
        meridianPrefs.Save();
    }

    public override void LoadMeridian()
    {
        level = meridianPrefs.GetInt("shaolinlevel");
        hp = meridianPrefs.GetInt("shaolinhp");
        mp = meridianPrefs.GetInt("shaolinmp");
        externalDefense = meridianPrefs.GetInt("shaolinexternalDefense");
        hpRegen = meridianPrefs.GetInt("shaolinhpRegen");
        skipExternalDefense = meridianPrefs.GetInt("shaolinskipExternalDefense");
    }

    public override void UpdatePropertyData()
    {
        propertyData["Khí huyeát:"] = hp.ToString();
        propertyData["Noäi löïc:"] = mp.ToString();
        propertyData["Ngoaïi phoøng:"] = externalDefense.ToString();
        propertyData["Hoài khí huyeát:"] = hpRegen.ToString();
        propertyData["Boû qua ngoaïi phoøng:"] = skipExternalDefense.ToString();
    }

    public override void SaveCharacterData()
    {
        characterPrefs.SetInt("hp", characterPrefs.GetInt("hp") + 1 );
        characterPrefs.SetInt("mp", characterPrefs.GetInt("mp") + 1 );
        characterPrefs.SetInt("externalDefense", characterPrefs.GetInt("externalDefense") + 1 );
        characterPrefs.SetInt("hpRegen", characterPrefs.GetInt("hpRegen") + 1 );
        characterPrefs.SetInt("skipExternalDefense", characterPrefs.GetInt("skipExternalDefense") + 1 );
        characterPrefs.Save();
    }
}
