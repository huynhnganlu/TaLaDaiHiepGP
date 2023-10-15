using System.Collections.Generic;

public class WudangMeridian : MeridianAbstract
{

    private void Start()
    {
        propertyData = new Dictionary<string, string>
        {
            { "Khí huyeát:", hp.ToString()},
            { "Noäi löïc:", mp.ToString()},
            { "Hoài noäi löïc:", mpRegen.ToString()},
            { "Hoài khí huyeát:", hpRegen.ToString()},
            { "Uy löïc noäi coâng:", internalDamage.ToString()},
        };

    }
    public override void LevelUpMeridian()
    {
        if (level < 36 && characterPrefs.GetInt("qi") - (5 * level) >= 0)
        {
            level++;
            hp += 8;
            mp += 2;
            mpRegen += 1;
            hpRegen += 1;
            internalDamage += 2;

            UpdatePropertyData();
            GetPropertyData();
            SaveMeridian();
            SaveCharacterData();
        }
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
        if (meridianPrefs.HasKey("wudanglevel"))
            level = meridianPrefs.GetInt("wudanglevel");
        else
            level = 1;
        hp = meridianPrefs.GetInt("wudanghp");
        mp = meridianPrefs.GetInt("wudangmp");
        mpRegen = meridianPrefs.GetInt("wudangmpRegen");
        hpRegen = meridianPrefs.GetInt("wudanghpRegen");
        internalDamage = meridianPrefs.GetInt("wudanginternalDamage");
    }

    public override void UpdatePropertyData()
    {
        propertyData["Khí huyeát:"] = hp.ToString();
        propertyData["Noäi löïc:"] = mp.ToString();
        propertyData["Hoài noäi löïc:"] = mpRegen.ToString();
        propertyData["Hoài khí huyeát:"] = hpRegen.ToString();
        propertyData["Uy löïc noäi coâng:"] = internalDamage.ToString();
        characterPrefs.SetInt("qi", characterPrefs.GetInt("qi") - (5 * level));
        characterPrefs.Save();

    }

    public override void SaveCharacterData()
    {
        characterPrefs.SetInt("hp", characterPrefs.GetInt("hp") + 8);
        characterPrefs.SetInt("mp", characterPrefs.GetInt("mp") + 2);
        characterPrefs.SetInt("mpRegen", characterPrefs.GetInt("mpRegen") + 1);
        characterPrefs.SetInt("hpRegen", characterPrefs.GetInt("hpRegen") + 1);
        characterPrefs.SetInt("internalDamage", characterPrefs.GetInt("internalDamage") + 2);
        characterPrefs.Save();
    }
}
