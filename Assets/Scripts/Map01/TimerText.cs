using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TimerText : MonoBehaviour
{
    private float time = 600f;
    public TextMeshProUGUI text;
    private float minute = 0f;
    private float second = 0f;
    // Start is called before the first frame update

    void Start()
    {
        if(time > 60f)
        {
            minute = time / 60f;
            second = time % 60f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(second <= 0f)
        {
            minute -= 1f;
            second = 59f;
        }
        second -= 1 * Time.deltaTime;
        text.text = minute.ToString("0") + ":" + second.ToString("0");
    }
}
