using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThxForPlaying : MonoBehaviour
{
    private void Start()
    {
        AudioMusicManager.Instance.StopMusic();
        StartCoroutine(WaitABit());
    }
    private IEnumerator WaitABit()
    {
        yield return new WaitForSecondsRealtime(22f);

        GameManager.Instance.LoadNextLevel();
    }
}
