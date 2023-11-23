using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingController : MonoBehaviour
{
    public Slider slider;
    public GameObject loadingHolder;
    private JsonPlayerPrefs characterPrefs;
    public static LoadingController Instance;
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        characterPrefs = new JsonPlayerPrefs(Application.persistentDataPath + "/CharacterData.json");
    }

    private IEnumerator Start()
    {
        yield return LocalizationSettings.InitializationOperation;      
        if(characterPrefs.HasKey("settinglg"))
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[characterPrefs.GetInt("settinglg")];
    }


    public void LoadLevel(string scene)
    {
        loadingHolder.SetActive(true);
        StartCoroutine(LoadAsyncScene(scene));
    }

    IEnumerator LoadAsyncScene(string scene)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            yield return null;
        }
    }
}
