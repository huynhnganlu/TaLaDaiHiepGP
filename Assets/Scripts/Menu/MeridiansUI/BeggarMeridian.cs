using System.Collections.Generic;

public class BeggarMeridian : MeridianAbstract
{


    private void Start()
    {;
        propertyData = new Dictionary<string, string>
        {
            { "Khí huyeát:", hp.ToString()},
            { "Noäi löïc:", mp.ToString()},
            { "Phoøng ngöï:", defense.ToString()},
            { "Uy löïc noäi coâng:", internalDamage.ToString()},
            { "Uy löïc ngoaïi coâng:", externalDamage.ToString()},

        };
    }

    public override void LevelUpMeridian()
    {
        if (level < 36 && characterPrefs.GetInt("qi") - (5 * level) >= 0) 
        {
            level++;
            hp += 8;
            mp += 2;
            internalDamage += 2;
            defense += 1;
            externalDamage += 2;
            UpdatePropertyData();
            GetPropertyData();
            SaveMeridian();
            SaveCharacterData();
        }
    }

    public override void SaveMeridian()
    {
        meridianPrefs.SetInt("beggarlevel", level);
        meridianPrefs.SetInt("beggarhp", hp);
        meridianPrefs.SetInt("beggarmp", mp);
        meridianPrefs.SetInt("beggarinternalDamage", internalDamage);
        meridianPrefs.SetInt("beggardefense", defense);
        meridianPrefs.SetInt("beggarexternalDamage", externalDamage);
        meridianPrefs.Save();
    }

    public override void LoadMeridian()
    {
        if (meridianPrefs.HasKey("beggarlevel"))
            level = meridianPrefs.GetInt("beggarlevel");
        else
            level = 1;
        hp = meridianPrefs.GetInt("beggarhp");
        mp = meridianPrefs.GetInt("beggarmp");
        internalDamage = meridianPrefs.GetInt("beggarinternalDamage");
        defense = meridianPrefs.GetInt("beggardefense");
        externalDamage = meridianPrefs.GetInt("beggarexternalDamage");
    }

    public override void UpdatePropertyData()
    {
        propertyData["Khí huyeát:"] = hp.ToString();
        propertyData["Noäi löïc:"] = mp.ToString();
        propertyData["Phoøng ngöï:"] = defense.ToString();
        propertyData["Uy löïc noäi coâng:"] = internalDamage.ToString();
        propertyData["Uy löïc ngoaïi coâng:"] = externalDamage.ToString();
        characterPrefs.SetInt("qi", characterPrefs.GetInt("qi") - (5 * level));
        characterPrefs.Save();

    }

    public override void SaveCharacterData()
    {
        characterPrefs.SetInt("hp", characterPrefs.GetInt("hp") + 8);
        characterPrefs.SetInt("mp", characterPrefs.GetInt("mp") + 2);
        characterPrefs.SetInt("internalDamage", characterPrefs.GetInt("internalDamage") + 2);
        characterPrefs.SetInt("defense", characterPrefs.GetInt("defense") + 1);
        characterPrefs.SetInt("externalDamage", characterPrefs.GetInt("externalDamage") + 2);
        characterPrefs.Save();
    }
}
