using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingController : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private GameObject loadingHolder;

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
