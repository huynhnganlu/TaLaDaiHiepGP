using System.Collections.Generic;

public class ShaolinMeridian : MeridianAbstract
{


    private void Start()
    {
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
        if (level < 36 && characterPrefs.GetInt("qi") - (5 * level) >= 0)
        {
            level++;
            hp += 2;
            mp += 2;
            defense += 1;
            hpRegen += 1;
            movementSpeed += 0.02f;

            UpdatePropertyData();
            GetPropertyData();
            SaveMeridian();
            SaveCharacterData();
            AudioManager.Instance.PlaySE("LevelUpSE");

        }
    }

    public override void SaveMeridian()
    {
        meridianPrefs.SetInt("shaolinlevel", level);
        meridianPrefs.SetInt("shaolinhp", hp);
        meridianPrefs.SetInt("shaolinmp", mp);
        meridianPrefs.SetInt("shaolindefense", defense);
        meridianPrefs.SetInt("shaolinhpRegen", hpRegen);
        meridianPrefs.SetFloat("shaolinmovementSpeed", System.MathF.Round(movementSpeed, 2));
        meridianPrefs.Save();
    }

    public override void LoadMeridian()
    {
        if (meridianPrefs.HasKey("shaolinlevel"))
            level = meridianPrefs.GetInt("shaolinlevel");
        else
            level = 1;
        hp = meridianPrefs.GetInt("shaolinhp");
        mp = meridianPrefs.GetInt("shaolinmp");
        defense = meridianPrefs.GetInt("shaolindefense");
        hpRegen = meridianPrefs.GetInt("shaolinhpRegen");
        movementSpeed = meridianPrefs.GetFloat("shaolinmovementSpeed");
    }

    public override void UpdatePropertyData()
    {
        propertyData["Kh� huye�t:"] = hp.ToString();
        propertyData["No�i l��c:"] = mp.ToString();
        propertyData["Pho�ng ng��:"] = defense.ToString();
        propertyData["Ho�i kh� huye�t:"] = hpRegen.ToString();
        propertyData["To�c �o� di chuye�n:"] = System.MathF.Round(movementSpeed,2).ToString();
        characterPrefs.SetInt("qi", characterPrefs.GetInt("qi") - (5 * level));
        characterPrefs.Save();

    }

    public override void SaveCharacterData()
    {
        characterPrefs.SetInt("hp", characterPrefs.GetInt("hp") + 2);
        characterPrefs.SetInt("mp", characterPrefs.GetInt("mp") + 2);
        characterPrefs.SetInt("defense", characterPrefs.GetInt("defense") + 1);
        characterPrefs.SetInt("hpRegen", characterPrefs.GetInt("hpRegen") + 1);
        characterPrefs.SetFloat("movementSpeed", System.MathF.Round(characterPrefs.GetFloat("movementSpeed") + 0.02f, 2));
        characterPrefs.Save();
    }
}
