using System.Collections.Generic;

public class TangmenMeridian : MeridianAbstract
{

    private void Start()
    {
        propertyData = new Dictionary<string, string>
        {
            { "Kh� huye�t:", hp.ToString()},
            { "No�i l��c:", mp.ToString()},
            { "Ne� tra�nh:", evade.ToString()},
            { "Uy l��c ngoa�i co�ng:", externalDamage.ToString()},
            { "T� le� ba�o k�ch:", critRate.ToString()},
        };
    }
    public override void LevelUpMeridian()
    {
        if (level < 36 && characterPrefs.GetInt("qi") - (5 * level) >= 0)
        {
            level++;
            hp += 8;
            mp += 2;
            evade += 0.5f;
            externalDamage += 2;
            critRate += 0.5f;

            UpdatePropertyData();
            GetPropertyData();
            SaveMeridian();
            SaveCharacterData();
        }
    }

    public override void SaveMeridian()
    {
        meridianPrefs.SetInt("tangmenlevel", level);
        meridianPrefs.SetInt("tangmenhp", hp);
        meridianPrefs.SetInt("tangmenmp", mp);
        meridianPrefs.SetFloat("tangmenevade", evade);
        meridianPrefs.SetInt("tangmenexternalDamage", externalDamage);
        meridianPrefs.SetFloat("tangmencritRate", critRate);
        meridianPrefs.Save();
    }

    public override void LoadMeridian()
    {
        if (meridianPrefs.HasKey("tangmenlevel"))
            level = meridianPrefs.GetInt("tangmenlevel");
        else
            level = 1;
        hp = meridianPrefs.GetInt("tangmenhp");
        mp = meridianPrefs.GetInt("tangmenmp");
        evade = meridianPrefs.GetFloat("tangmenevade");
        externalDamage = meridianPrefs.GetInt("tangmenexternalDamage");
        critRate = meridianPrefs.GetFloat("tangmencritRate");
    }

    public override void UpdatePropertyData()
    {
        propertyData["Kh� huye�t:"] = hp.ToString();
        propertyData["No�i l��c:"] = mp.ToString();
        propertyData["Ne� tra�nh:"] = evade.ToString();
        propertyData["Uy l��c ngoa�i co�ng:"] = externalDamage.ToString();
        propertyData["T� le� ba�o k�ch:"] = critRate.ToString();
        characterPrefs.SetInt("qi", characterPrefs.GetInt("qi") - (5 * level));
        characterPrefs.Save();

    }

    public override void SaveCharacterData()
    {
        characterPrefs.SetInt("hp", characterPrefs.GetInt("hp") + 8);
        characterPrefs.SetInt("mp", characterPrefs.GetInt("mp") + 2);
        characterPrefs.SetFloat("evade", characterPrefs.GetFloat("evade") + 0.5f);
        characterPrefs.SetInt("externalDamage", characterPrefs.GetInt("externalDamage") + 2);
        characterPrefs.SetFloat("critRate", characterPrefs.GetFloat("critRate") + 0.5f);
        characterPrefs.Save();
    }
}
