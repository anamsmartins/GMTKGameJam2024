using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } = null;

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

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // Main Menu

        if (PlayerPrefs.HasKey("MasterVolume"))
        {
            float masterVolume = PlayerPrefs.GetFloat("MasterVolume");
            AudioMasterManager.Instance.ChangeMasterVolume(masterVolume);
        }
        
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            float musicVolume = PlayerPrefs.GetFloat("MusicVolume");
            AudioMusicManager.Instance.ChangeMusicVolume(musicVolume);
        }

        if (PlayerPrefs.HasKey("SoundsVolume"))
        {
            float soundsVolume = PlayerPrefs.GetFloat("SoundsVolume");
            AudioSoundsManager.Instance.ChangeSoundsVolume(soundsVolume);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex > 0) // if not main menu
        {

        }
    }

    private IEnumerator LoadSceneAsync(int sceneIndex)
    {
        string typeMusic = "Menu";
        if (sceneIndex != 0) {
            typeMusic = "Level";
        }
        AudioMusicManager.Instance.PlayMusic(typeMusic);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        if (sceneIndex != 0)
        {
            SaveGame();
        }
    }



    public void LoadNextLevel()
    {
        int targetBuildIndex = SceneManager.GetActiveScene().buildIndex;

        if (++targetBuildIndex >= SceneManager.sceneCountInBuildSettings)
        {
            targetBuildIndex = 0;
        }
        StartCoroutine(LoadSceneAsync(targetBuildIndex));
    }

    public void LoadSavedGame()
    {
        if (PlayerPrefs.HasKey("SavedScene"))
        {
            int savedScene = PlayerPrefs.GetInt("SavedScene");

            Debug.Log(savedScene);

            StartCoroutine(LoadSceneAsync(savedScene));
        }
        else
        {
            // Handle the case where no save exists
            Debug.Log("No saved game found.");
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1;
    }

    public void SaveGame()
    {
        // Save the current scene name
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("SavedScene", currentSceneIndex);
        PlayerPrefs.SetFloat("MusicVolume", AudioMusicManager.Instance.musicVolume);
        PlayerPrefs.SetFloat("MasterVolume", AudioMasterManager.Instance.masterVolume);
        PlayerPrefs.SetFloat("SoundsVolume", AudioSoundsManager.Instance.soundsVolume);


        PlayerPrefs.Save(); // Ensure the data is written to disk
    }

    public void LoadMainMenuScreen()
    {
        StartCoroutine(LoadSceneAsync(0));
    }
}