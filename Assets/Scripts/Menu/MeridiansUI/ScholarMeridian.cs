using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScholarMeridian : MeridianAbstract
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
            { "Uy l��c no�i co�ng:", internalDamage.ToString()},
            { "To�c �o� di chuye�n:", movementSpeed.ToString()},
            { "T� le� ba�o k�ch:", critRate.ToString()},
        };   
    }

    public override void LevelUpMeridian()
    {
        level++;
        hp += 1;
        mp += 1;
        internalDamage += 1;
        movementSpeed += 1;
        critRate += 1;

        UpdatePropertyData();
        GetPropertyData();
        SaveMeridian();
        SaveCharacterData();
    }

    public override void SaveMeridian() 
    {
        meridianPrefs.SetInt("scholarlevel", level);
        meridianPrefs.SetInt("scholarhp", hp);
        meridianPrefs.SetInt("scholarmp", mp);
        meridianPrefs.SetInt("scholarinternalDamage", internalDamage);
        meridianPrefs.SetInt("scholarmovementSpeed", movementSpeed);
        meridianPrefs.SetInt("scholarcritRate", critRate);
        meridianPrefs.Save();
    }

    public override void LoadMeridian()
    {
        level = meridianPrefs.GetInt("scholarlevel");
        hp = meridianPrefs.GetInt("scholarhp");
        mp = meridianPrefs.GetInt("scholarmp");
        internalDamage = meridianPrefs.GetInt("scholarinternalDamage");
        movementSpeed = meridianPrefs.GetInt("scholarmovementSpeed");
        critRate = meridianPrefs.GetInt("scholarcritRate");
    }

    public override void UpdatePropertyData()
    {
        propertyData["Kh� huye�t:"] = hp.ToString();
        propertyData["No�i l��c:"] = mp.ToString();
        propertyData["Uy l��c no�i co�ng:"] = internalDamage.ToString();
        propertyData["To�c �o� di chuye�n:"] = movementSpeed.ToString();
        propertyData["T� le� ba�o k�ch:"] = critRate.ToString();
    }

    public override void SaveCharacterData()
    {
        characterPrefs.SetInt("hp", characterPrefs.GetInt("hp") + 1 );
        characterPrefs.SetInt("mp", characterPrefs.GetInt("mp") + 1 );
        characterPrefs.SetInt("internalDamage", characterPrefs.GetInt("internalDamage") + 1);
        characterPrefs.SetInt("movementSpeed", characterPrefs.GetInt("movementSpeed") + 1 );
        characterPrefs.SetInt("critRate", characterPrefs.GetInt("critRate") + 1 );
        characterPrefs.Save();
    }
}
