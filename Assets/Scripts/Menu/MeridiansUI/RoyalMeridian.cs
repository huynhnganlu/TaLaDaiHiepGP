using System.Collections.Generic;

public class RoyalMeridian : MeridianAbstract
{


    private void Start()
    {
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
        if (level < 36 && characterPrefs.GetInt("qi") - (5 * level) >= 0)
        {
            level++;
            hp += 8;
            mp += 2;
            externalDamage += 2;
            critDamage += 2;
            critRate += 0.5f;

            UpdatePropertyData();
            GetPropertyData();
            SaveMeridian();
            SaveCharacterData();
        }
    }

    public override void SaveMeridian()
    {
        meridianPrefs.SetInt("royallevel", level);
        meridianPrefs.SetInt("royalhp", hp);
        meridianPrefs.SetInt("royalmp", mp);
        meridianPrefs.SetInt("royalexternalDamage", externalDamage);
        meridianPrefs.SetInt("royalcritDamage", critDamage);
        meridianPrefs.SetFloat("royalcritRate", critRate);
        meridianPrefs.Save();
    }

    public override void LoadMeridian()
    {
        if (meridianPrefs.HasKey("royallevel"))
            level = meridianPrefs.GetInt("royallevel");
        else
            level = 1;
        hp = meridianPrefs.GetInt("royalhp");
        mp = meridianPrefs.GetInt("royalmp");
        externalDamage = meridianPrefs.GetInt("royalexternalDamage");
        critDamage = meridianPrefs.GetInt("royalcritDamage");
        critRate = meridianPrefs.GetFloat("royalcritRate");
    }

    public override void UpdatePropertyData()
    {
        propertyData["Kh� huye�t:"] = hp.ToString();
        propertyData["No�i l��c:"] = mp.ToString();
        propertyData["Uy l��c ngoa�i co�ng:"] = externalDamage.ToString();
        propertyData["Sa�t th��ng ba�o k�ch:"] = critDamage.ToString();
        propertyData["T� le� ba�o k�ch:"] = critRate.ToString();
        characterPrefs.SetInt("qi", characterPrefs.GetInt("qi") - (5 * level));
        characterPrefs.Save();

    }

    public override void SaveCharacterData()
    {
        characterPrefs.SetInt("hp", characterPrefs.GetInt("hp") + 8);
        characterPrefs.SetInt("mp", characterPrefs.GetInt("mp") + 2);
        characterPrefs.SetInt("externalDamage", characterPrefs.GetInt("externalDamage") + 2);
        characterPrefs.SetInt("critDamage", characterPrefs.GetInt("critDamage") + 2);
        characterPrefs.SetFloat("critRate", characterPrefs.GetFloat("critRate") + 0.5f);
        characterPrefs.Save();
    }
}
