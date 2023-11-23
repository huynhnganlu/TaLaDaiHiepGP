using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class IntroController : MonoBehaviour
{
    private void Start()
    {
        GetComponent<VideoPlayer>().loopPointReached += LoadMainmenu;
    }

    private void LoadMainmenu(VideoPlayer vp)
    {
        gameObject.SetActive(false);
        LoadingController.Instance.LoadLevel("MainMenu");

    }
}
