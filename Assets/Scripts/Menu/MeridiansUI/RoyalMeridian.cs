using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoyalMeridian : MeridianAbstract
{
    private JsonPlayerPrefs prefs;

    private void Start()
    {
        prefs = MeridianController.Instance.prefs;
        LoadMeridian();
        propertyData = new Dictionary<string, string>
        {
            { "Kh� huye�t:", hp.ToString()},
            { "No�i l��c:", mp.ToString()},
            { "Uy l��c ngoa�i co�ng:", externalDamage.ToString()},
            { "Bo� qua ngoa�i pho�ng:", skipExternalDefense.ToString()},
            { "Ngoa�i co�ng ba�o k�ch:", externalCrit.ToString()},
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
    }

    public override void SaveMeridian()
    {
        prefs.SetInt("royallevel", level);
        prefs.SetInt("royalhp", hp);
        prefs.SetInt("royalmp", mp);
        prefs.SetInt("royalexternalDamage", externalDamage);
        prefs.SetInt("royalskipExternalDefense", skipExternalDefense);
        prefs.SetInt("royalexternalCrit", externalCrit);
        prefs.Save();
    }

    public override void LoadMeridian()
    {
        level = prefs.GetInt("royallevel");
        hp = prefs.GetInt("royalhp");
        mp = prefs.GetInt("royalmp");
        externalDamage = prefs.GetInt("royalexternalDamage");
        skipExternalDefense = prefs.GetInt("royalskipExternalDefense");
        externalCrit = prefs.GetInt("royalexternalCrit");
    }

    public override void UpdatePropertyData()
    {
        propertyData["Kh� huye�t:"] = hp.ToString();
        propertyData["No�i l��c:"] = mp.ToString();
        propertyData["Uy l��c ngoa�i co�ng:"] = externalDamage.ToString();
        propertyData["Bo� qua ngoa�i pho�ng:"] = skipExternalDefense.ToString();
        propertyData["Ngoa�i co�ng ba�o k�ch:"] = externalCrit.ToString();
    }
}
