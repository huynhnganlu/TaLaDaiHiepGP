using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class SettingsMenuController : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown languageDropdown, windowModeDropdown, resolutionDropdown;
    [SerializeField]
    private Slider masterSoundSlider, musicSlider, soundSlider;
    [SerializeField]
    private TextMeshProUGUI masterSoundText, musicText, soundText;

    private int width = 1920, height = 1080;
    private bool fullscreen = true;

    //Su dung IEnumerator de InitializationOperation duoc khoi tao nham dam bao Localization hoat dong

    private void Awake()
    {
        SetScreenResolution(width, height, fullscreen);
    }

    IEnumerator Start()
    {
        yield return LocalizationSettings.InitializationOperation;

        if (languageDropdown != null)
            languageDropdown.onValueChanged.AddListener(LanguageChanged);

        masterSoundSlider.value = 100;
        musicSlider.value = 100;
        soundSlider.value = 100;

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
        if (slider.name.Equals("MasterSoundSlider"))
        {
            foreach(Sound sound in AudioManager.Instance.BGsounds)
            {
                sound.source.volume = slider.value / 200f;
            }
            foreach (Sound sound in AudioManager.Instance.SEsounds)
            {
                sound.source.volume = slider.value / 100f;
            }
        }
        else if (slider.name.Equals("MusicSlider"))
        {
            foreach (Sound sound in AudioManager.Instance.BGsounds)
            {
                sound.source.volume = slider.value / 200f;
            }
        }
        else if (slider.name.Equals("SoundSlider"))
        {
            foreach (Sound sound in AudioManager.Instance.SEsounds)
            {
                sound.source.volume = slider.value / 100f;
            }
        }
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

}
