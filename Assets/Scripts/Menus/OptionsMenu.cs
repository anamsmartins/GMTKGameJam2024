using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    public static OptionsMenu Instance { get; private set; } = null;

    [SerializeField] private GameObject CloseButton;
    [SerializeField] private GameObject QuitButton;
    [SerializeField] private GameObject CreditsCanvas;


    private void Start()
    {
        HideCreditsCanvas();
    }

    public void OnClose()
    {
        AudioSoundsManager.Instance.PlaySoundPressButton();
        UIManager.Instance.OnCloseOptions();
    }

    public void OnQuit()
    {
        AudioSoundsManager.Instance.PlaySoundPressButton();
        UIManager.Instance.OnCloseOptions();
        GameManager.Instance.LoadMainMenuScreen();
    }

    public void OnCredits()
    {
        AudioSoundsManager.Instance.PlaySoundPressButton();
        ShowCreditsCanvas();
    }

    public void OnBackCredits()
    {
        AudioSoundsManager.Instance.PlaySoundPressButton();
        HideCreditsCanvas();
    }

    private void ShowCreditsCanvas()
    {
        CloseButton.SetActive(false);
        QuitButton.SetActive(false);
        CreditsCanvas.SetActive(true);
    }

    private void HideCreditsCanvas()
    {
        CloseButton.SetActive(true);
        QuitButton.SetActive(true);
        CreditsCanvas.SetActive(false);
    }

}
