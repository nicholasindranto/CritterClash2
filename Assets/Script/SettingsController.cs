using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    // reference ke slider sama dropdown nya
    public Slider audioSlider;
    private float currentSlider = 0f;

    // Start is called before the first frame update
    void Start()
    {
        // load valuenya ke slider dan dropdownnya
        audioSlider.value = PlayerPrefs.GetFloat("audio", 1f);

        // set on value change biar otomatis
        audioSlider.onValueChanged.AddListener(OnAudioChanged);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnAudioChanged(float value)
    {
        PlayerPrefs.SetFloat("audio", value);
        PlayerPrefs.Save();
    }
}
