using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class EmeiMeridian : MeridianAbstract
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
            { "Noäi phoøng:", internalDefense.ToString()},
            { "Boû qua noäi phoøng:", skipInternalDefense.ToString()}

        };
        GetPropertyData();
    }

    public override void LevelUpMeridian()
    {
        level++;
        hp += 1;
        mp += 1;
        evade += 1;
        internalDefense += 1;
        skipInternalDefense += 1;

        UpdatePropertyData();
        GetPropertyData();
        SaveMeridian();
        SaveCharacterData();
    }

    public override void SaveMeridian()
    {
        meridianPrefs.SetInt("emeilevel", level);
        meridianPrefs.SetInt("emeihp", hp);
        meridianPrefs.SetInt("emeimp", mp);
        meridianPrefs.SetInt("emeievade", evade);
        meridianPrefs.SetInt("emeiinternalDefense", internalDefense);
        meridianPrefs.SetInt("emeiskipInternalDefense", skipInternalDefense);
        meridianPrefs.Save();
    }

    public override void LoadMeridian()
    {
        level = meridianPrefs.GetInt("emeilevel");
        hp = meridianPrefs.GetInt("emeihp");
        mp = meridianPrefs.GetInt("emeimp");
        evade = meridianPrefs.GetInt("emeievade");
        internalDefense = meridianPrefs.GetInt("emeiinternalDefense");
        skipInternalDefense = meridianPrefs.GetInt("emeiskipInternalDefense");
    }

    public override void UpdatePropertyData()
    {
        propertyData["Khí huyeát:"] = hp.ToString();
        propertyData["Noäi löïc:"] = mp.ToString();
        propertyData["Neù traùnh:"] = evade.ToString();
        propertyData["Noäi phoøng:"] = internalDefense.ToString();
        propertyData["Boû qua noäi phoøng:"] = skipInternalDefense.ToString();
    }

    public override void SaveCharacterData()
    {
        characterPrefs.SetInt("hp", characterPrefs.GetInt("hp") + 1);
        characterPrefs.SetInt("mp", characterPrefs.GetInt("mp") + 1);
        characterPrefs.SetInt("evade", characterPrefs.GetInt("evade") + 1);
        characterPrefs.SetInt("internalDefense", characterPrefs.GetInt("internalDefense") + 1);
        characterPrefs.SetInt("skipInternalDefense", characterPrefs.GetInt("skipInternalDefense") + 1);
        characterPrefs.Save();
    }
}
