using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void OnClickNewGame()
    {
        AudioSoundsManager.Instance.PlaySoundPressButton();
        Debug.Log($"{name}: New Game");

        // If game was saved, load the indexSceneToLoad
        PlayerPrefs.SetInt("SavedScene", 0);
        LevelChanger.Instance.FadeToLevel("Next");
        //StartCoroutine(LoadSceneAsync(indexSceneToLoad));
    }

    public void OnClickContinueGame()
    {
        AudioSoundsManager.Instance.PlaySoundPressButton();
        Debug.Log($"{name}: Continue Game");

        // If game was saved, load the indexSceneToLoad
        LevelChanger.Instance.FadeToLevel("ContinueGame");
        //StartCoroutine(LoadSceneAsync(indexSceneToLoad));
    }

    public void OnClickOptions()
    {
        AudioSoundsManager.Instance.PlaySoundPressButton();
        Debug.Log($"{name}: Options");
        UIManager.Instance.OnClickOptions();
    }

    public void OnClickCredits()
    {
        Debug.Log($"{name}: Credits");
    }

    public void OnClickQuit()
    {
        AudioSoundsManager.Instance.PlaySoundPressButton();
        Debug.Log($"{name}: Quit");
        Application.Quit();
    }
}
