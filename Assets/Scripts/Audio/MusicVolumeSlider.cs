using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MusicVolumeSlider : MonoBehaviour
{
    private Slider volumeSlider;

    private void Awake()
    {
        volumeSlider = GetComponent<Slider>();
    }

    private void Start()
    {
        volumeSlider.value = AudioMusicManager.Instance.musicVolume;
        volumeSlider.onValueChanged.AddListener(OnMusicSliderValueChanged);
    }

    private void OnMusicSliderValueChanged(float newValue)
    {
        AudioMusicManager.Instance.ChangeMusicVolume(newValue);
    }

}
