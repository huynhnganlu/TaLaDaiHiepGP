using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValleyMeridian : MeridianAbstract
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
        SaveCharacterData();
    }

    public override void SaveMeridian()
    {
        meridianPrefs.SetInt("valleylevel", level);
        meridianPrefs.SetInt("valleyhp", hp);
        meridianPrefs.SetInt("valleymp", mp);
        meridianPrefs.SetInt("valleympRegen", mpRegen);
        meridianPrefs.SetInt("valleyskipInternalDefense", skipInternalDefense);
        meridianPrefs.SetInt("valleyinternalCrit", internalCrit);
        meridianPrefs.Save();
    }

    public override void LoadMeridian()
    {
        level = meridianPrefs.GetInt("valleylevel");
        hp = meridianPrefs.GetInt("valleyhp");
        mp = meridianPrefs.GetInt("valleymp");
        mpRegen = meridianPrefs.GetInt("valleympRegen");
        skipInternalDefense = meridianPrefs.GetInt("valleyskipInternalDefense");
        internalCrit = meridianPrefs.GetInt("valleyinternalCrit");
    }

    public override void UpdatePropertyData()
    {
        propertyData["Kh� huye�t:"] = hp.ToString();
        propertyData["No�i l��c:"] = mp.ToString();
        propertyData["Ho�i no�i l��c:"] = mpRegen.ToString();
        propertyData["Bo� qua no�i pho�ng:"] = skipInternalDefense.ToString();
        propertyData["No�i co�ng ba�o k�ch:"] = internalCrit.ToString();
    }

    public override void SaveCharacterData()
    {
        characterPrefs.SetInt("hp", characterPrefs.GetInt("hp") + 1 );
        characterPrefs.SetInt("mp", characterPrefs.GetInt("mp") + 1 );
        characterPrefs.SetInt("mpRegen", characterPrefs.GetInt("mpRegen") + 1 );
        characterPrefs.SetInt("skipInternalDefense", characterPrefs.GetInt("skipInternalDefense") + 1 );
        characterPrefs.SetInt("internalCrit", characterPrefs.GetInt("internalCrit") + 1 );
        characterPrefs.Save();
    }
}
