using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioMusicManager : MonoBehaviour
{
    public static AudioMusicManager Instance { get; private set; } = null;

    [HideInInspector] public float musicVolume = 1;

    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip levelsMusic;

    [SerializeField] private AudioMixer myAudioMixer = null;

    private AudioSource myAudioSource;

    private void Awake()
    {
        myAudioSource = GetComponent<AudioSource>();
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            if (SceneManager.GetActiveScene().buildIndex != 4)
            {
                myAudioSource.loop = true;
                myAudioSource.clip = menuMusic;
                myAudioSource.Play();
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic(string musicType)
    {
        if (SceneManager.GetActiveScene().buildIndex != 4)
        {
            // If the new clip is different from the current one

            if (musicType == "Menu" && myAudioSource.clip.name != "Menu music")
            {
                StartCoroutine(SwitchMusic(menuMusic));
            }

            if (musicType == "Level" && myAudioSource.clip.name != "game music")
            {
                StartCoroutine(SwitchMusic(levelsMusic));
            }
        }
    }

    private IEnumerator SwitchMusic(AudioClip newClip)
    {
        // Fade out current music if it's playing
        if (myAudioSource.isPlaying)
        {
            for (float volume = 1f; volume >= 0; volume -= 0.1f)
            {
                myAudioSource.volume = volume;
                yield return new WaitForSeconds(0.1f);
            }
            myAudioSource.Stop();
        }

        // Change the clip and play the new music
        myAudioSource.clip = newClip;
        myAudioSource.mute = false;
        myAudioSource.Play();

        // Fade in the new music
        for (float volume = 0; volume <= 1f; volume += 0.1f)
        {
            myAudioSource.volume = volume;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void ChangeMusicVolume(float volume) 
    {
        musicVolume = volume;
        myAudioMixer.SetFloat("MusicVolume", volume);
    }

    public float GetCurrentMusicVolume()
    {
        return musicVolume;
    }

    public void StopMusic()
    {
        StartCoroutine(StopMusicWait());
    }

    private IEnumerator StopMusicWait()
    {
        yield return new WaitForSecondsRealtime(1f);
        Debug.Log("STOOOOOP");
        myAudioSource.mute = true;

    }
}
