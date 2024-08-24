using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSoundsManager : MonoBehaviour
{
    public static AudioSoundsManager Instance { get; private set; } = null;

    [HideInInspector] public float soundsVolume = 1;

    [SerializeField] private AudioMixer myAudioMixer = null;

    [SerializeField] private AudioClip AudioClipPressButtons = null;

    // Puzzle 1
    [Header("Puzzle1")]
    [SerializeField] private AudioClip AudioClipCrocodileJoke = null;
    [SerializeField] private AudioClip AudioClipDontTouchSleepingRat = null;
    [SerializeField] private AudioClip AudioClipLevelPass = null;
    [SerializeField] private AudioClip AudioClipMouseSqueak = null;
    [SerializeField] private AudioClip AudioClipNoPassage = null;
    [SerializeField] private AudioClip AudioClipPickRat = null;
    [SerializeField] private AudioClip AudioClipPutRatDown = null;
    [SerializeField] private AudioClip AudioClipWalking = null;

    // Puzzle 2
    [Header("Puzzle2")]
    [SerializeField] private AudioClip AudioClipJumpOnRat = null;
    [SerializeField] private AudioClip AudioClipLandJump = null;
    [SerializeField] private AudioClip AudioClipPlump = null;
    [SerializeField] private AudioClip AudioClipTree = null;
    [SerializeField] private AudioClip AudioClipWeedKillerAnim = null;

    // Puzzle 3
    [Header("Puzzle3")]
    [SerializeField] private AudioClip AudioClipLockUnlocks = null;
    [SerializeField] private AudioClip AudioClipMovePuzzlePiece = null;
    [SerializeField] private AudioClip AudioClipTouchLock = null;

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

    public void ChangeSoundsVolume(float volume)
    {
        soundsVolume = volume;
        myAudioMixer.SetFloat("SoundsVolume", volume);
    }

    public float GetCurrentSoundsVolume()
    {
        return soundsVolume;
    }

    public void PlaySoundPressButton()
    {
        audioSource.PlayOneShot(AudioClipPressButtons);
    }

    public void PlaySoundCrocodileJoke()
    {
        audioSource.PlayOneShot(AudioClipCrocodileJoke);
    }

    public void PlaySoundDontTouchSleepingRat()
    {
        audioSource.PlayOneShot(AudioClipDontTouchSleepingRat);
    }

    public void PlaySoundLevelPass()
    {
        audioSource.PlayOneShot(AudioClipLevelPass);
    }

    public void PlaySoundMouseSqueak()
    {
        audioSource.PlayOneShot(AudioClipMouseSqueak);
    }

    public void PlaySoundNoPassage()
    {
        audioSource.PlayOneShot(AudioClipNoPassage);
    }

    public void PlaySoundPickRat()
    {
        audioSource.PlayOneShot(AudioClipPickRat);
    }

    public void PlaySoundPutRat()
    {
        audioSource.PlayOneShot(AudioClipPutRatDown);
    }

    public void PlaySoundWalking()
    {
        audioSource.PlayOneShot(AudioClipWalking);
    }

    public void PlaySoundJumpOnRat()
    {
        audioSource.PlayOneShot(AudioClipJumpOnRat);
    }

    public void PlaySoundLandJump()
    {
        audioSource.PlayOneShot(AudioClipLandJump);
    }

    public void PlaySoundPlump()
    {
        audioSource.PlayOneShot(AudioClipPlump);
    }

    public void PlaySoundTree()
    {
        audioSource.PlayOneShot(AudioClipTree);
    }

    public void PlaySoundWeedKillerAnim()
    {
        audioSource.PlayOneShot(AudioClipWeedKillerAnim);
    }

    public void PlaySoundLockUnlock()
    {
        audioSource.PlayOneShot(AudioClipLockUnlocks);
    }

    public void PlaySoundMovePuzzlePiece()
    {
        audioSource.PlayOneShot(AudioClipMovePuzzlePiece);
    }

    public void PlaySoundTouchLock()
    {
        audioSource.PlayOneShot(AudioClipTouchLock);
    }
}
