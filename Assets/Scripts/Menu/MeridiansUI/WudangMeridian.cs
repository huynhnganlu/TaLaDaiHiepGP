using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WudangMeridian : MeridianAbstract
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
            { "Ho�i no�i l��c:", mpRegen.ToString()},
            { "No�i co�ng ba�o k�ch:", internalCrit.ToString()},
            { "Ngoa�i co�ng ba�o k�ch:", externalCrit.ToString()},
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
    }

    public override void SaveMeridian()
    {
        prefs.SetInt("wudanglevel", level);
        prefs.SetInt("wudanghp", hp);
        prefs.SetInt("wudangmp", mp);
        prefs.SetInt("wudangmpRegen", mpRegen);
        prefs.SetInt("wudanginternalCrit", internalCrit);
        prefs.SetInt("wudangexternalCrit", externalCrit);
        prefs.Save();
    }

    public override void LoadMeridian()
    {
        level = prefs.GetInt("wudanglevel");
        hp = prefs.GetInt("wudanghp");
        mp = prefs.GetInt("wudangmp");
        mpRegen = prefs.GetInt("wudangmpRegen");
        internalCrit = prefs.GetInt("wudanginternalCrit");
        externalCrit = prefs.GetInt("wudangexternalCrit");
    }

    public override void UpdatePropertyData()
    {
        propertyData["Kh� huye�t:"] = hp.ToString();
        propertyData["No�i l��c:"] = mp.ToString();
        propertyData["Ho�i no�i l��c:"] = mpRegen.ToString();
        propertyData["No�i co�ng ba�o k�ch:"] = internalCrit.ToString();
        propertyData["Ngoa�i co�ng ba�o k�ch:"] = externalCrit.ToString();
    }
}
