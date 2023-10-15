using System.Collections.Generic;

public class EmeiMeridian : MeridianAbstract
{


    private void Start()
    {
        propertyData = new Dictionary<string, string>
        {
            { "Khí huyeát:", hp.ToString()},
            { "Noäi löïc:", mp.ToString()},
            { "Neù traùnh:", evade.ToString()},
            { "Phoøng ngöï:", defense.ToString()},
            { "Saùt thöông baïo kích:", critDamage.ToString()}

        };
        GetPropertyData();
    }

    public override void LevelUpMeridian()
    {
        if (level < 36 && characterPrefs.GetInt("qi") - (level * 5) >= 0)
        {
            level++;
            hp += 8;
            mp += 2;
            evade += 0.5f;
            defense += 1;
            critDamage += 2;

            UpdatePropertyData();
            GetPropertyData();
            SaveMeridian();
            SaveCharacterData();
        }
    }

    public override void SaveMeridian()
    {
        meridianPrefs.SetInt("emeilevel", level);
        meridianPrefs.SetInt("emeihp", hp);
        meridianPrefs.SetInt("emeimp", mp);
        meridianPrefs.SetFloat("emeievade", evade);
        meridianPrefs.SetInt("emeidefense", defense);
        meridianPrefs.SetInt("emeicritDamage", critDamage);
        meridianPrefs.Save();
    }

    public override void LoadMeridian()
    {
        if (meridianPrefs.HasKey("emeilevel"))
            level = meridianPrefs.GetInt("emeilevel");
        else
            level = 1;
        hp = meridianPrefs.GetInt("emeihp");
        mp = meridianPrefs.GetInt("emeimp");
        evade = meridianPrefs.GetFloat("emeievade");
        defense = meridianPrefs.GetInt("emeidefense");
        critDamage = meridianPrefs.GetInt("emeicritDamage");
    }

    public override void UpdatePropertyData()
    {
        propertyData["Khí huyeát:"] = hp.ToString();
        propertyData["Noäi löïc:"] = mp.ToString();
        propertyData["Neù traùnh:"] = evade.ToString();
        propertyData["Phoøng ngöï:"] = defense.ToString();
        propertyData["Saùt thöông baïo kích:"] = critDamage.ToString();
        characterPrefs.SetInt("qi", characterPrefs.GetInt("qi") - (level *5));
        characterPrefs.Save();

    }

    public override void SaveCharacterData()
    {
        characterPrefs.SetInt("hp", characterPrefs.GetInt("hp") + 8);
        characterPrefs.SetInt("mp", characterPrefs.GetInt("mp") + 2);
        characterPrefs.SetFloat("evade", characterPrefs.GetFloat("evade") + 0.5f);
        characterPrefs.SetInt("defense", characterPrefs.GetInt("defense") + 1);
        characterPrefs.SetInt("critDamage", characterPrefs.GetInt("critDamage") + 2);
        characterPrefs.Save();
    }
}
