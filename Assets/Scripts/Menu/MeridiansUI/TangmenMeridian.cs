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
            { "Kh� huye�t:", hp.ToString()},
            { "No�i l��c:", mp.ToString()},
            { "Ne� tra�nh:", evade.ToString()},
            { "Uy l��c ngoa�i co�ng:", externalDamage.ToString()},
            { "Ngoa�i co�ng ba�o k�ch:", externalCrit.ToString()},
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
        propertyData["Kh� huye�t:"] = hp.ToString();
        propertyData["No�i l��c:"] = mp.ToString();
        propertyData["Ne� tra�nh:"] = evade.ToString();
        propertyData["Uy l��c ngoa�i co�ng:"] = externalDamage.ToString();
        propertyData["Ngoa�i co�ng ba�o k�ch:"] = externalCrit.ToString();
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
