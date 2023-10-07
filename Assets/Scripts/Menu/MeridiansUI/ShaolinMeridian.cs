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
            { "Khí huyeát:", hp.ToString()},
            { "Noäi löïc:", mp.ToString()},
            { "Phoøng ngöï:", defense.ToString()},
            { "Hoài khí huyeát:", hpRegen.ToString()},
            { "Toác ñoä di chuyeån:", movementSpeed.ToString()},
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
        propertyData["Khí huyeát:"] = hp.ToString();
        propertyData["Noäi löïc:"] = mp.ToString();
        propertyData["Phoøng ngöï:"] = defense.ToString();
        propertyData["Hoài khí huyeát:"] = hpRegen.ToString();
        propertyData["Toác ñoä di chuyeån:"] = movementSpeed.ToString();
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
