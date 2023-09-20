using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValleyMeridian : MeridianAbstract
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
            { "Bo� qua no�i pho�ng:", skipInternalDefense.ToString()},
            { "No�i co�ng ba�o k�ch:", internalCrit.ToString()},

        };

    }

    public override void LevelUpMeridian()
    {
        level++;
        hp += 1;
        mp += 1;
        mpRegen += 1;
        skipInternalDefense += 1;
        internalCrit += 1;

        UpdatePropertyData();
        GetPropertyData();
        SaveMeridian();
    }

    public override void SaveMeridian()
    {
        prefs.SetInt("valleylevel", level);
        prefs.SetInt("valleyhp", hp);
        prefs.SetInt("valleymp", mp);
        prefs.SetInt("valleympRegen", mpRegen);
        prefs.SetInt("valleyskipInternalDefense", skipInternalDefense);
        prefs.SetInt("valleyinternalCrit", internalCrit);
        prefs.Save();
    }

    public override void LoadMeridian()
    {
        level = prefs.GetInt("valleylevel");
        hp = prefs.GetInt("valleyhp");
        mp = prefs.GetInt("valleymp");
        mpRegen = prefs.GetInt("valleympRegen");
        skipInternalDefense = prefs.GetInt("valleyskipInternalDefense");
        internalCrit = prefs.GetInt("valleyinternalCrit");
    }

    public override void UpdatePropertyData()
    {
        propertyData["Kh� huye�t:"] = hp.ToString();
        propertyData["No�i l��c:"] = mp.ToString();
        propertyData["Ho�i no�i l��c:"] = mpRegen.ToString();
        propertyData["Bo� qua no�i pho�ng:"] = skipInternalDefense.ToString();
        propertyData["No�i co�ng ba�o k�ch:"] = internalCrit.ToString();
    }
}
