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
            { "Khí huyeát:", hp.ToString()},
            { "Noäi löïc:", mp.ToString()},
            { "Hoài noäi löïc:", mpRegen.ToString()},
            { "Boû qua noäi phoøng:", skipInternalDefense.ToString()},
            { "Noäi coâng baïo kích:", internalCrit.ToString()},

        };

    }

    public override void LevelUpMeridian()
    {
        level++;
        hp += 1;
        mp += 1;
        mpRegen += 1;
        skipInternalDefense += 1;
        internalCrit += 1;

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
        meridianPrefs.SetInt("valleyskipInternalDefense", skipInternalDefense);
        meridianPrefs.SetInt("valleyinternalCrit", internalCrit);
        meridianPrefs.Save();
    }

    public override void LoadMeridian()
    {
        level = meridianPrefs.GetInt("valleylevel");
        hp = meridianPrefs.GetInt("valleyhp");
        mp = meridianPrefs.GetInt("valleymp");
        mpRegen = meridianPrefs.GetInt("valleympRegen");
        skipInternalDefense = meridianPrefs.GetInt("valleyskipInternalDefense");
        internalCrit = meridianPrefs.GetInt("valleyinternalCrit");
    }

    public override void UpdatePropertyData()
    {
        propertyData["Khí huyeát:"] = hp.ToString();
        propertyData["Noäi löïc:"] = mp.ToString();
        propertyData["Hoài noäi löïc:"] = mpRegen.ToString();
        propertyData["Boû qua noäi phoøng:"] = skipInternalDefense.ToString();
        propertyData["Noäi coâng baïo kích:"] = internalCrit.ToString();
    }

    public override void SaveCharacterData()
    {
        characterPrefs.SetInt("hp", characterPrefs.GetInt("hp") + 1 );
        characterPrefs.SetInt("mp", characterPrefs.GetInt("mp") + 1 );
        characterPrefs.SetInt("mpRegen", characterPrefs.GetInt("mpRegen") + 1 );
        characterPrefs.SetInt("skipInternalDefense", characterPrefs.GetInt("skipInternalDefense") + 1 );
        characterPrefs.SetInt("internalCrit", characterPrefs.GetInt("internalCrit") + 1 );
        characterPrefs.Save();
    }
}
