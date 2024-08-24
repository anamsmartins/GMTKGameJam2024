using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioGrassManager : MonoBehaviour
{
    public static AudioGrassManager Instance { get; private set; } = null;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

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

    public void PlaySoundWalking()
    {
        audioSource.Play();
    }

    public void StopSoundWalking()
    {
        audioSource.Stop();
    }

    public bool IsPlaying { get {  return audioSource.isPlaying; } }
}
