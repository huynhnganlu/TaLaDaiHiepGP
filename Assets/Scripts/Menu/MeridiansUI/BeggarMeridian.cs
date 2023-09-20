using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeggarMeridian : MeridianAbstract
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
            { "No�i pho�ng:", internalDefense.ToString()},
            { "Uy l��c no�i co�ng:", internalDamage.ToString()},
            { "No�i co�ng ba�o k�ch:", internalCrit.ToString()},

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
    }

    public override void SaveMeridian()
    {
        prefs.SetInt("beggarlevel", level);
        prefs.SetInt("beggarhp", hp);
        prefs.SetInt("beggarmp", mp);
        prefs.SetInt("beggarinternalDamage", internalDamage);
        prefs.SetInt("beggarinternalDefense", internalDefense);
        prefs.SetInt("beggarinternalCrit", internalCrit);
        prefs.Save();
    }

    public override void LoadMeridian()
    {
        level = prefs.GetInt("beggarlevel");
        hp = prefs.GetInt("beggarhp");
        mp = prefs.GetInt("beggarmp");
        internalDamage = prefs.GetInt("beggarinternalDamage");
        internalDefense = prefs.GetInt("beggarinternalDefense");
        internalCrit = prefs.GetInt("beggarinternalCrit");
    }

    public override void UpdatePropertyData()
    {
        propertyData["Kh� huye�t:"] = hp.ToString();
        propertyData["No�i l��c:"] = mp.ToString();
        propertyData["No�i pho�ng:"] = internalDefense.ToString();
        propertyData["Uy l��c no�i co�ng:"] = internalDamage.ToString();
        propertyData["No�i co�ng ba�o k�ch:"] = internalCrit.ToString();
    }
}
