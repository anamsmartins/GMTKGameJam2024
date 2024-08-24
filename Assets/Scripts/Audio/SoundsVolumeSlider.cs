using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundsVolumeSlider : MonoBehaviour
{
    private Slider volumeSlider;

    private void Awake()
    {
        volumeSlider = GetComponent<Slider>();
    }

    private void Start()
    {
        volumeSlider.value = AudioSoundsManager.Instance.soundsVolume;
        volumeSlider.onValueChanged.AddListener(OnSoundsSliderValueChanged);
    }

    private void OnSoundsSliderValueChanged(float newValue)
    {
        AudioSoundsManager.Instance.ChangeSoundsVolume(newValue);
    }
}
