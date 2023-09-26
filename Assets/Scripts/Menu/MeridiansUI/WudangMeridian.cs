using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WudangMeridian : MeridianAbstract
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
            { "Hoài noäi löïc:", mpRegen.ToString()},
            { "Noäi coâng baïo kích:", internalCrit.ToString()},
            { "Ngoaïi coâng baïo kích:", externalCrit.ToString()},
        };

    }
    public override void LevelUpMeridian()
    {
        level++;
        hp += 1;
        mp += 1;
        mpRegen += 1;
        internalCrit += 1;
        externalCrit += 1;

        UpdatePropertyData();
        GetPropertyData();
        SaveMeridian();
        SaveCharacterData();
    }

    public override void SaveMeridian()
    {
        meridianPrefs.SetInt("wudanglevel", level);
        meridianPrefs.SetInt("wudanghp", hp);
        meridianPrefs.SetInt("wudangmp", mp);
        meridianPrefs.SetInt("wudangmpRegen", mpRegen);
        meridianPrefs.SetInt("wudanginternalCrit", internalCrit);
        meridianPrefs.SetInt("wudangexternalCrit", externalCrit);
        meridianPrefs.Save();
    }

    public override void LoadMeridian()
    {
        level = meridianPrefs.GetInt("wudanglevel");
        hp = meridianPrefs.GetInt("wudanghp");
        mp = meridianPrefs.GetInt("wudangmp");
        mpRegen = meridianPrefs.GetInt("wudangmpRegen");
        internalCrit = meridianPrefs.GetInt("wudanginternalCrit");
        externalCrit = meridianPrefs.GetInt("wudangexternalCrit");
    }

    public override void UpdatePropertyData()
    {
        propertyData["Khí huyeát:"] = hp.ToString();
        propertyData["Noäi löïc:"] = mp.ToString();
        propertyData["Hoài noäi löïc:"] = mpRegen.ToString();
        propertyData["Noäi coâng baïo kích:"] = internalCrit.ToString();
        propertyData["Ngoaïi coâng baïo kích:"] = externalCrit.ToString();
    }

    public override void SaveCharacterData()
    {
        characterPrefs.SetInt("hp", characterPrefs.GetInt("hp") + 1);
        characterPrefs.SetInt("mp", characterPrefs.GetInt("mp") + 1);
        characterPrefs.SetInt("mpRegen", characterPrefs.GetInt("mpRegen") + 1);
        characterPrefs.SetInt("internalCrit", characterPrefs.GetInt("internalCrit") + 1);
        characterPrefs.SetInt("externalCrit", characterPrefs.GetInt("externalCrit") + 1);
        characterPrefs.Save();
    }
}
