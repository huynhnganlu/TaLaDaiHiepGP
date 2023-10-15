using System.Collections.Generic;

public class ScholarMeridian : MeridianAbstract
{


    private void Start()
    {
        propertyData = new Dictionary<string, string>
        {
            { "Khí huyeát:", hp.ToString()},
            { "Noäi löïc:", mp.ToString()},
            { "Uy löïc noäi coâng:", internalDamage.ToString()},
            { "Toác ñoä di chuyeån:", movementSpeed.ToString()},
            { "Tæ leä baïo kích:", critRate.ToString()},
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
            movementSpeed += 0.01f;
            critRate += 0.5f;

            UpdatePropertyData();
            GetPropertyData();
            SaveMeridian();
            SaveCharacterData();
        }
    }

    public override void SaveMeridian()
    {
        meridianPrefs.SetInt("scholarlevel", level);
        meridianPrefs.SetInt("scholarlevel", level);
        meridianPrefs.SetInt("scholarhp", hp);
        meridianPrefs.SetInt("scholarmp", mp);
        meridianPrefs.SetInt("scholarinternalDamage", internalDamage);
        meridianPrefs.SetFloat("scholarmovementSpeed", System.MathF.Round(movementSpeed, 2));
        meridianPrefs.SetFloat("scholarcritRate", critRate);
        meridianPrefs.Save();
    }

    public override void LoadMeridian()
    {
        if (meridianPrefs.HasKey("scholarlevel"))
            level = meridianPrefs.GetInt("scholarlevel");
        else
            level = 1;
        hp = meridianPrefs.GetInt("scholarhp");
        mp = meridianPrefs.GetInt("scholarmp");
        internalDamage = meridianPrefs.GetInt("scholarinternalDamage");
        movementSpeed = meridianPrefs.GetFloat("scholarmovementSpeed");
        critRate = meridianPrefs.GetFloat("scholarcritRate");
    }

    public override void UpdatePropertyData()
    {
        propertyData["Khí huyeát:"] = hp.ToString();
        propertyData["Noäi löïc:"] = mp.ToString();
        propertyData["Uy löïc noäi coâng:"] = internalDamage.ToString();
        propertyData["Toác ñoä di chuyeån:"] = System.MathF.Round(movementSpeed, 2).ToString();
        propertyData["Tæ leä baïo kích:"] = critRate.ToString();
        characterPrefs.SetInt("qi", characterPrefs.GetInt("qi") - (5 * level));
        characterPrefs.Save();
    }


    public override void SaveCharacterData()
    {
        characterPrefs.SetInt("hp", characterPrefs.GetInt("hp") + 8);
        characterPrefs.SetInt("mp", characterPrefs.GetInt("mp") + 2);
        characterPrefs.SetInt("internalDamage", characterPrefs.GetInt("internalDamage") + 2);
        characterPrefs.SetFloat("movementSpeed", System.MathF.Round(characterPrefs.GetFloat("movementSpeed") + 0.01f, 2));
        characterPrefs.SetFloat("critRate", characterPrefs.GetFloat("critRate") + 0.5f);
        characterPrefs.Save();
    }
}
