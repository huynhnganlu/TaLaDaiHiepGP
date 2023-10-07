using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoyalMeridian : MeridianAbstract
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
            { "Uy l��c ngoa�i co�ng:", externalDamage.ToString()},
            { "Sa�t th��ng ba�o k�ch:", critDamage.ToString()},
            { "T� le� ba�o k�ch:", critRate.ToString()},
        };
    }

    public override void LevelUpMeridian()
    {
        level++;
        hp += 1;
        mp += 1;
        externalDamage += 1;
        critDamage += 1;
        critRate += 1;

        UpdatePropertyData();
        GetPropertyData();
        SaveMeridian();
        SaveCharacterData();
    }

    public override void SaveMeridian()
    {
        meridianPrefs.SetInt("royallevel", level);
        meridianPrefs.SetInt("royalhp", hp);
        meridianPrefs.SetInt("royalmp", mp);
        meridianPrefs.SetInt("royalexternalDamage", externalDamage);
        meridianPrefs.SetInt("royalcritDamage", critDamage);
        meridianPrefs.SetInt("royalcritRate", critRate);
        meridianPrefs.Save();
    }

    public override void LoadMeridian()
    {
        level = meridianPrefs.GetInt("royallevel");
        hp = meridianPrefs.GetInt("royalhp");
        mp = meridianPrefs.GetInt("royalmp");
        externalDamage = meridianPrefs.GetInt("royalexternalDamage");
        critDamage = meridianPrefs.GetInt("royalcritDamage");
        critRate = meridianPrefs.GetInt("royalcritRate");
    }

    public override void UpdatePropertyData()
    {
        propertyData["Kh� huye�t:"] = hp.ToString();
        propertyData["No�i l��c:"] = mp.ToString();
        propertyData["Uy l��c ngoa�i co�ng:"] = externalDamage.ToString();
        propertyData["Sa�t th��ng ba�o k�ch:"] = critDamage.ToString();
        propertyData["T� le� ba�o k�ch:"] = critRate.ToString();
    }

    public override void SaveCharacterData()
    {
        characterPrefs.SetInt("hp", characterPrefs.GetInt("hp") + 1);
        characterPrefs.SetInt("mp", characterPrefs.GetInt("mp") + 1 );
        characterPrefs.SetInt("externalDamage", characterPrefs.GetInt("externalDamage") + 1 );
        characterPrefs.SetInt("critDamage", characterPrefs.GetInt("critDamage") + 1 );
        characterPrefs.SetInt("critRate", characterPrefs.GetInt("critRate") + 1 );
        characterPrefs.Save();
    }
}
