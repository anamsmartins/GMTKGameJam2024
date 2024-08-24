using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MasterVolumeSlider : MonoBehaviour
{
    private Slider volumeSlider;

    private void Awake()
    {
        volumeSlider = GetComponent<Slider>();
    }

    private void Start()
    {
        volumeSlider.value = AudioMasterManager.Instance.masterVolume;
        volumeSlider.onValueChanged.AddListener(OnMasterSliderValueChanged);
    }

    private void OnMasterSliderValueChanged(float newValue)
    {
        AudioMasterManager.Instance.ChangeMasterVolume(newValue);
    }
}
