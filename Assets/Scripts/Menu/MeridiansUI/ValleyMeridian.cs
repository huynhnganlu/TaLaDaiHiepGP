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
            { "Pho�ng ng��:", defense.ToString()},
            { "Sa�t th��ng ba�o k�ch:", critDamage.ToString()},

        };

    }

    public override void LevelUpMeridian()
    {
        level++;
        hp += 1;
        mp += 1;
        mpRegen += 1;
        defense += 1;
        critDamage += 1;

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
        meridianPrefs.SetInt("valleydefense", defense);
        meridianPrefs.SetInt("valleycritDamage", critDamage);
        meridianPrefs.Save();
    }

    public override void LoadMeridian()
    {
        level = meridianPrefs.GetInt("valleylevel");
        hp = meridianPrefs.GetInt("valleyhp");
        mp = meridianPrefs.GetInt("valleymp");
        mpRegen = meridianPrefs.GetInt("valleympRegen");
        defense = meridianPrefs.GetInt("valleydefense");
        critDamage = meridianPrefs.GetInt("valleycritDamage");
    }

    public override void UpdatePropertyData()
    {
        propertyData["Kh� huye�t:"] = hp.ToString();
        propertyData["No�i l��c:"] = mp.ToString();
        propertyData["Ho�i no�i l��c:"] = mpRegen.ToString();
        propertyData["Pho�ng ng��:"] = defense.ToString();
        propertyData["Sa�t th��ng ba�o k�ch:"] = critDamage.ToString();
    }

    public override void SaveCharacterData()
    {
        characterPrefs.SetInt("hp", characterPrefs.GetInt("hp") + 1 );
        characterPrefs.SetInt("mp", characterPrefs.GetInt("mp") + 1 );
        characterPrefs.SetInt("mpRegen", characterPrefs.GetInt("mpRegen") + 1 );
        characterPrefs.SetInt("defense", characterPrefs.GetInt("defense") + 1 );
        characterPrefs.SetInt("critDamage", characterPrefs.GetInt("critDamage") + 1 );
        characterPrefs.Save();
    }
}
