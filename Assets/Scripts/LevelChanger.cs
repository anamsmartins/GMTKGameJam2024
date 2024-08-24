
using UnityEngine;

public class LevelChanger : MonoBehaviour
{
    public static LevelChanger Instance { get; private set; } = null;

    private Animator myAnimator;
    private string levelConfig;

    private void Awake()
    {
        if (Instance == null)
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
        myAnimator = GetComponent<Animator>();
        Time.timeScale = 1;
    }


    public void FadeToLevel(string config)
    {
        levelConfig = config;
        myAnimator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        if (levelConfig == "ContinueGame")
        {
            GameManager.Instance.LoadSavedGame();
        }
        else
        {
            GameManager.Instance.LoadNextLevel();
        }
    }
}
