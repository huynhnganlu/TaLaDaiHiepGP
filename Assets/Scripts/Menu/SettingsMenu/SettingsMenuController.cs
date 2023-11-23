using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class SettingsMenuController : MonoBehaviour
{
    public static SettingsMenuController Instance;
    [SerializeField]
    private TMP_Dropdown languageDropdown, windowModeDropdown, resolutionDropdown;
    [SerializeField]
    private Slider masterSoundSlider, musicSlider, soundSlider;
    [SerializeField]
    private TextMeshProUGUI masterSoundText, musicText, soundText;

    private int width, height;
    private bool fullscreen;

    //Su dung IEnumerator de InitializationOperation duoc khoi tao nham dam bao Localization hoat dong

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    IEnumerator Start()
    {
        yield return LocalizationSettings.InitializationOperation;

        if (languageDropdown != null)
            languageDropdown.onValueChanged.AddListener(LanguageChanged);

        masterSoundSlider.onValueChanged.AddListener(delegate
        {
            SliderValueChanged(masterSoundSlider, masterSoundText);
        });

        musicSlider.onValueChanged.AddListener(delegate
        {
            SliderValueChanged(musicSlider, musicText);
        });

        soundSlider.onValueChanged.AddListener(delegate
        {
            SliderValueChanged(soundSlider, soundText);
        });

        windowModeDropdown.onValueChanged.AddListener(WindowModeValueChanged);

        resolutionDropdown.onValueChanged.AddListener(ResolutionValueChanged);
    }

    public void LanguageChanged(int index)
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
    }

    public void SliderValueChanged(Slider slider, TextMeshProUGUI text)
    {
        text.text = slider.value.ToString();
        AudioManager.Instance.SettingAudio(masterSoundSlider.value, musicSlider.value, soundSlider.value);
    }

    public void WindowModeValueChanged(int change)
    {
        switch (change)
        {
            case 0:
                fullscreen = true;
                break;
            case 1:
                fullscreen = false;
                break;
        }

        SetScreenResolution(width, height, fullscreen);
    }

    public void ResolutionValueChanged(int change)
    {
        switch (change)
        {
            case 0:
                width = 1920;
                height = 1080;
                break;
            case 1:
                width = 1600;
                height = 900;
                break;
        }

        SetScreenResolution(width, height, fullscreen);
    }

    public void SetScreenResolution(int _width, int _height, bool _fullScreen)
    {
        Screen.SetResolution(_width, _height, _fullScreen);
    }

    public void SaveSettings()
    {
        MenuController.Instance.characterPrefs.SetInt("settingmaster", (int)masterSoundSlider.value);
        MenuController.Instance.characterPrefs.SetInt("settingbgm", (int)musicSlider.value);
        MenuController.Instance.characterPrefs.SetInt("settingsfx", (int)soundSlider.value);
        MenuController.Instance.characterPrefs.SetInt("settingres", resolutionDropdown.value);
        MenuController.Instance.characterPrefs.SetInt("settingwd", windowModeDropdown.value);
        MenuController.Instance.characterPrefs.SetInt("settinglg", languageDropdown.value);
        MenuController.Instance.characterPrefs.Save();
    }

    public void LoadSettings()
    {
        int res = MenuController.Instance.characterPrefs.GetInt("settingres");
        int wd = MenuController.Instance.characterPrefs.GetInt("settingwd");
        int lg = MenuController.Instance.characterPrefs.GetInt("settinglg");
        switch(res)
        {
            case 0:
                width = 1920;
                height = 1080;
                break;
            case 1:
                width = 1600;
                height = 900;
                break;
        }
        switch (wd)
        {
            case 0:
                fullscreen = true;
                break;
            case 1:
                fullscreen = false;
                break;
        }
        SetScreenResolution(width, height, fullscreen);
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[lg];
    }

    public void LoadSettingsText()
    {
        masterSoundText.text = MenuController.Instance.characterPrefs.GetInt("settingmaster").ToString();
        soundText.text = MenuController.Instance.characterPrefs.GetInt("settingsfx").ToString();
        musicText.text = MenuController.Instance.characterPrefs.GetInt("settingbgm").ToString();
        masterSoundSlider.value = MenuController.Instance.characterPrefs.GetInt("settingmaster");
        soundSlider.value = MenuController.Instance.characterPrefs.GetInt("settingsfx");
        musicSlider.value = MenuController.Instance.characterPrefs.GetInt("settingbgm");
        resolutionDropdown.value = MenuController.Instance.characterPrefs.GetInt("settingres");
        windowModeDropdown.value = MenuController.Instance.characterPrefs.GetInt("settingwd");
        languageDropdown.value = MenuController.Instance.characterPrefs.GetInt("settinglg");
    }
}
