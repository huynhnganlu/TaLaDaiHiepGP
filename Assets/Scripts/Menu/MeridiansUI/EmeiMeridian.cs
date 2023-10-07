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
            { "Kh� huye�t:", hp.ToString()},
            { "No�i l��c:", mp.ToString()},
            { "Ne� tra�nh:", evade.ToString()},
            { "Pho�ng ng��:", defense.ToString()},
            { "Sa�t th��ng ba�o k�ch:", critDamage.ToString()}

        };
        GetPropertyData();
    }

    public override void LevelUpMeridian()
    {
        level++;
        hp += 1;
        mp += 1;
        evade += 1;
        defense += 1;
        critDamage += 1;

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
        meridianPrefs.SetInt("emeidefense", defense);
        meridianPrefs.SetInt("emeicritDamage", critDamage);
        meridianPrefs.Save();
    }

    public override void LoadMeridian()
    {
        level = meridianPrefs.GetInt("emeilevel");
        hp = meridianPrefs.GetInt("emeihp");
        mp = meridianPrefs.GetInt("emeimp");
        evade = meridianPrefs.GetInt("emeievade");
        defense = meridianPrefs.GetInt("emeidefense");
        critDamage = meridianPrefs.GetInt("emeicritDamage");
    }

    public override void UpdatePropertyData()
    {
        propertyData["Kh� huye�t:"] = hp.ToString();
        propertyData["No�i l��c:"] = mp.ToString();
        propertyData["Ne� tra�nh:"] = evade.ToString();
        propertyData["Pho�ng ng��:"] = defense.ToString();
        propertyData["Sa�t th��ng ba�o k�ch:"] = critDamage.ToString();
    }

    public override void SaveCharacterData()
    {
        characterPrefs.SetInt("hp", characterPrefs.GetInt("hp") + 1);
        characterPrefs.SetInt("mp", characterPrefs.GetInt("mp") + 1);
        characterPrefs.SetInt("evade", characterPrefs.GetInt("evade") + 1);
        characterPrefs.SetInt("defense", characterPrefs.GetInt("defense") + 1);
        characterPrefs.SetInt("critDamage", characterPrefs.GetInt("critDamage") + 1);
        characterPrefs.Save();
    }
}
