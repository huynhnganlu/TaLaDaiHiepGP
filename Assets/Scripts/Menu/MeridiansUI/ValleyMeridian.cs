using System.Collections.Generic;
using UnityEngine.Localization.Settings;

public class ValleyMeridian : MeridianAbstract
{


    private void OnEnable()
    {
        if (LocalizationSettings.SelectedLocale.Equals(LocalizationSettings.AvailableLocales.GetLocale("en")))
        {
            propertyData = new Dictionary<string, string>
            {
                { "Health:", hp.ToString()},
                { "Mana:", mp.ToString()},
                { "MP regen:", mpRegen.ToString()},
                { "Defense:", defense.ToString()},
                { "Crit damage:", critDamage.ToString()},

            };
        }
        else
        {
            propertyData = new Dictionary<string, string>
            {
                { "Kh� huye�t:", hp.ToString()},
                { "No�i l��c:", mp.ToString()},
                { "Ho�i no�i l��c:", mpRegen.ToString()},
                { "Pho�ng ng��:", defense.ToString()},
                { "Sa�t th��ng ba�o k�ch:", critDamage.ToString()},

            };
        }
      

    }

    public override void LevelUpMeridian()
    {
        if (level < 36 && characterPrefs.GetInt("qi") - (5 * level) >= 0)
        {
            level++;
            hp += 2;
            mp += 2;
            mpRegen += 1;
            defense += 1;
            critDamage += 1;

            UpdatePropertyData();
            GetPropertyData();
            SaveMeridian();
            SaveCharacterData();
            AudioManager.Instance.PlaySE("LevelUpSE");

        }
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
        if (meridianPrefs.HasKey("valleylevel"))
            level = meridianPrefs.GetInt("valleylevel");
        else
            level = 1;
        hp = meridianPrefs.GetInt("valleyhp");
        mp = meridianPrefs.GetInt("valleymp");
        mpRegen = meridianPrefs.GetInt("valleympRegen");
        defense = meridianPrefs.GetInt("valleydefense");
        critDamage = meridianPrefs.GetInt("valleycritDamage");
    }

    public override void UpdatePropertyData()
    {
        if (LocalizationSettings.SelectedLocale.Equals(LocalizationSettings.AvailableLocales.GetLocale("en")))
        {
            propertyData["Health:"] = hp.ToString();
            propertyData["Mana:"] = mp.ToString();
            propertyData["MP regen:"] = mpRegen.ToString();
            propertyData["Defense:"] = defense.ToString();
            propertyData["Crit damage:"] = critDamage.ToString();
        }
        else
        {
            propertyData["Kh� huye�t:"] = hp.ToString();
            propertyData["No�i l��c:"] = mp.ToString();
            propertyData["Ho�i no�i l��c:"] = mpRegen.ToString();
            propertyData["Pho�ng ng��:"] = defense.ToString();
            propertyData["Sa�t th��ng ba�o k�ch:"] = critDamage.ToString();
        }
      
        characterPrefs.SetInt("qi", characterPrefs.GetInt("qi") - (5 * level));
        characterPrefs.Save();

    }

    public override void SaveCharacterData()
    {
        characterPrefs.SetInt("hp", characterPrefs.GetInt("hp") + 2);
        characterPrefs.SetInt("mp", characterPrefs.GetInt("mp") + 2);
        characterPrefs.SetInt("mpRegen", characterPrefs.GetInt("mpRegen") + 1);
        characterPrefs.SetInt("defense", characterPrefs.GetInt("defense") + 1);
        characterPrefs.SetInt("critDamage", characterPrefs.GetInt("critDamage") + 1);
        characterPrefs.Save();
    }
}
