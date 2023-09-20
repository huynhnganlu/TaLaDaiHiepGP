using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScholarMeridian : MeridianAbstract
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
            { "Uy l��c no�i co�ng:", internalDamage.ToString()},
            { "Bo� qua no�i pho�ng:", skipInternalDefense.ToString()},
            { "No�i co�ng ba�o k�ch:", internalCrit.ToString()},
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
    }

    public override void SaveMeridian()
    {
        prefs.SetInt("scholarlevel", level);
        prefs.SetInt("scholarhp", hp);
        prefs.SetInt("scholarmp", mp);
        prefs.SetInt("scholarinternalDamage", internalDamage);
        prefs.SetInt("scholarskipInternalDefense", skipInternalDefense);
        prefs.SetInt("scholarinternalCrit", internalCrit);
        prefs.Save();
    }

    public override void LoadMeridian()
    {
        level = prefs.GetInt("scholarlevel");
        hp = prefs.GetInt("scholarhp");
        mp = prefs.GetInt("scholarmp");
        internalDamage = prefs.GetInt("scholarinternalDamage");
        skipInternalDefense = prefs.GetInt("scholarskipInternalDefense");
        internalCrit = prefs.GetInt("scholarinternalCrit");
    }

    public override void UpdatePropertyData()
    {
        propertyData["Kh� huye�t:"] = hp.ToString();
        propertyData["No�i l��c:"] = mp.ToString();
        propertyData["Uy l��c no�i co�ng:"] = internalDamage.ToString();
        propertyData["Bo� qua no�i pho�ng:"] = skipInternalDefense.ToString();
        propertyData["No�i co�ng ba�o k�ch:"] = internalCrit.ToString();
    }
}
