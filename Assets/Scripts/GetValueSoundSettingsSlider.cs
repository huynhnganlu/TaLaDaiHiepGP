using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GetValueSoundSettingsSlider : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private TextMeshProUGUI soundValue;
    void Start()
    {
        slider.value = 100;
        slider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ValueChangeCheck()
    {
        soundValue.text = slider.value.ToString();
    }
}
