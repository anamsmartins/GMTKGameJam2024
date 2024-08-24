using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMasterManager : MonoBehaviour
{
    public static AudioMasterManager Instance { get; private set; } = null;

    [HideInInspector] public float masterVolume = 1;

    [SerializeField] private AudioMixer myAudioMixer = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ChangeMasterVolume(float volume)
    {
        masterVolume = volume;
        myAudioMixer.SetFloat("MasterVolume", volume);
    }

    public float GetCurrentMasterVolume()
    {
        return masterVolume;
    }
}
