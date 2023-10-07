using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaolinMeridian : MeridianAbstract
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
            { "Pho�ng ng��:", defense.ToString()},
            { "Ho�i kh� huye�t:", hpRegen.ToString()},
            { "To�c �o� di chuye�n:", movementSpeed.ToString()},
        };
    }

    public override void LevelUpMeridian()
    {
        level++;
        hp += 1;
        mp += 1;
        defense += 1;
        hpRegen += 1;
        movementSpeed += 1;

        UpdatePropertyData();
        GetPropertyData();
        SaveMeridian();
        SaveCharacterData();
    }

    public override void SaveMeridian()
    {
        meridianPrefs.SetInt("shaolinlevel", level);
        meridianPrefs.SetInt("shaolinhp", hp);
        meridianPrefs.SetInt("shaolinmp", mp);
        meridianPrefs.SetInt("shaolindefense", defense);
        meridianPrefs.SetInt("shaolinhpRegen", hpRegen);
        meridianPrefs.SetInt("shaolinmovementSpeed", movementSpeed);
        meridianPrefs.Save();
    }

    public override void LoadMeridian()
    {
        level = meridianPrefs.GetInt("shaolinlevel");
        hp = meridianPrefs.GetInt("shaolinhp");
        mp = meridianPrefs.GetInt("shaolinmp");
        defense = meridianPrefs.GetInt("shaolindefense");
        hpRegen = meridianPrefs.GetInt("shaolinhpRegen");
        movementSpeed = meridianPrefs.GetInt("shaolinmovementSpeed");
    }

    public override void UpdatePropertyData()
    {
        propertyData["Kh� huye�t:"] = hp.ToString();
        propertyData["No�i l��c:"] = mp.ToString();
        propertyData["Pho�ng ng��:"] = defense.ToString();
        propertyData["Ho�i kh� huye�t:"] = hpRegen.ToString();
        propertyData["To�c �o� di chuye�n:"] = movementSpeed.ToString();
    }

    public override void SaveCharacterData()
    {
        characterPrefs.SetInt("hp", characterPrefs.GetInt("hp") + 1 );
        characterPrefs.SetInt("mp", characterPrefs.GetInt("mp") + 1 );
        characterPrefs.SetInt("defense", characterPrefs.GetInt("defense") + 1 );
        characterPrefs.SetInt("hpRegen", characterPrefs.GetInt("hpRegen") + 1 );
        characterPrefs.SetInt("movementSpeed", characterPrefs.GetInt("movementSpeed") + 1 );
        characterPrefs.Save();
    }
}
