using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockGateAnimator : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private int spritePerFrame = 34;
    [SerializeField] private bool loop = true;
    [SerializeField] private bool destroyOnEnd = false;

    private int index = 0;
    private Image image;
    private int frame = 0;

    void Awake()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        if (Level1SceneManager.Instance.finishedScalePuzzle)
        {
            if (!loop && index == sprites.Length) return;
            frame++;
            if (frame < spritePerFrame) return;
            image.sprite = sprites[index];
            frame = 0;
            index++;
            if (index >= sprites.Length)
            {
                if (loop) index = 0;
                if (destroyOnEnd) Destroy(gameObject);

                StartCoroutine(WaitABit());
            }
        }
    }

    private IEnumerator WaitABit()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        LevelChanger.Instance.FadeToLevel("Netx");
    }
}
