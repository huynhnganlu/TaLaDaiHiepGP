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
            { "Kh� huye�t:", hp.ToString()},
            { "No�i l��c:", mp.ToString()},
            { "Ho�i no�i l��c:", mpRegen.ToString()},
            { "Ho�i kh� huye�t:", hpRegen.ToString()},
            { "Uy l��c no�i co�ng:", internalDamage.ToString()},
        };

    }
    public override void LevelUpMeridian()
    {
        level++;
        hp += 1;
        mp += 1;
        mpRegen += 1;
        hpRegen += 1;
        internalDamage += 1;

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
        meridianPrefs.SetInt("wudanghpRegen", hpRegen);
        meridianPrefs.SetInt("wudanginternalDamage", internalDamage);
        meridianPrefs.Save();
    }

    public override void LoadMeridian()
    {
        level = meridianPrefs.GetInt("wudanglevel");
        hp = meridianPrefs.GetInt("wudanghp");
        mp = meridianPrefs.GetInt("wudangmp");
        mpRegen = meridianPrefs.GetInt("wudangmpRegen");
        hpRegen = meridianPrefs.GetInt("wudanghpRegen");
        internalDamage = meridianPrefs.GetInt("wudanginternalDamage");
    }

    public override void UpdatePropertyData()
    {
        propertyData["Kh� huye�t:"] = hp.ToString();
        propertyData["No�i l��c:"] = mp.ToString();
        propertyData["Ho�i no�i l��c:"] = mpRegen.ToString();
        propertyData["Ho�i kh� huye�t:"] = hpRegen.ToString();
        propertyData["Uy l��c no�i co�ng:"] = internalDamage.ToString();
    }

    public override void SaveCharacterData()
    {
        characterPrefs.SetInt("hp", characterPrefs.GetInt("hp") + 1);
        characterPrefs.SetInt("mp", characterPrefs.GetInt("mp") + 1);
        characterPrefs.SetInt("mpRegen", characterPrefs.GetInt("mpRegen") + 1);
        characterPrefs.SetInt("hpRegen", characterPrefs.GetInt("hpRegen") + 1);
        characterPrefs.SetInt("internalDamage", characterPrefs.GetInt("internalDamage") + 1);
        characterPrefs.Save();
    }
}
