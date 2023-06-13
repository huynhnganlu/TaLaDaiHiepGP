using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class SetLanguage : MonoBehaviour
{
    // Start is called before the first frame update
    private Dropdown languageDropdown;

    IEnumerator Start()
    {
        yield return LocalizationSettings.InitializationOperation;

        languageDropdown = GetComponent<Dropdown>();
        if(languageDropdown != null)
            languageDropdown.onValueChanged.AddListener(LanguageChanged);
    }

    public void LanguageChanged(int index)
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
    }


}
