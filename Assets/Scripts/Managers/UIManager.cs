using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; } = null;

    [SerializeField] private GameObject puzzleMenu = null;
    [SerializeField] private GameObject optionsMenu;

    private float previousMusicVolume;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (Level1SceneManager.Instance.completedPuzzle && puzzleMenu.activeSelf)
        {
            OnClosePuzzle();
        }
    }

    public void OnClickPuzzle()
    {
        puzzleMenu.SetActive(true);
        previousMusicVolume = AudioMusicManager.Instance.GetCurrentMusicVolume();
        AudioMusicManager.Instance.ChangeMusicVolume(-10f);
        Time.timeScale = 0;

    }

    public void OnClosePuzzle()
    {
        AudioMusicManager.Instance.ChangeMusicVolume(previousMusicVolume);
        Time.timeScale = 1;
        puzzleMenu.SetActive(false);
    }

    public bool IsPuzzleOpen()
    {
        return puzzleMenu.activeSelf;
    }

    public void OnClickOptions()
    {
        optionsMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void OnCloseOptions()
    {
        optionsMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public bool IsOptionsOpen()
    {
        return optionsMenu.activeSelf;
    }
}
