using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlumpRat : MonoBehaviour, IInteractive
{
    [SerializeField] private Sprite[] plumpedRatImages;
    
    private Canvas interactionCanvas = null;
    private Canvas plumpCanvas = null;

    private SpriteRenderer mySpriteRender;

    private void Awake()
    {
        interactionCanvas = gameObject.transform.GetChild(0).GetComponent<Canvas>();
        plumpCanvas = gameObject.transform.GetChild(1).GetComponent<Canvas>();

        mySpriteRender = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        HideInteractionOpportunity();
        HidePlumpCanvas();
    }

    private void Update()
    {
        if (Level1SceneManager.Instance.activatedLastInteraction)
        {
            gameObject.SetActive(false);
        }

        if (Level1SceneManager.Instance.plumpCount == 3)
        {
            mySpriteRender.sprite = plumpedRatImages[0];
        }

        if (Level1SceneManager.Instance.plumpCount == 6)
        {
            mySpriteRender.sprite = plumpedRatImages[1];
        }

        if (Level1SceneManager.Instance.plumpCount == 9)
        {
            mySpriteRender.sprite = plumpedRatImages[2];

            HidePlumpCanvas();
            Level1SceneManager.Instance.isPlumping = false;
            StartCoroutine(WaitABitForSound());
        }
    }

    private IEnumerator WaitABitForSound()
    {
        yield return new WaitForSecondsRealtime(0.19f);
        AudioSoundsManager.Instance.PlaySoundJumpOnRat();

        yield return new WaitForSecondsRealtime(0.1f);
        AudioSoundsManager.Instance.PlaySoundLandJump();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Level1SceneManager.Instance.isHoldingPlump)
        {
            ShowInteractionOpportunity();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        HideInteractionOpportunity();
    }

    private void HidePlumpCanvas()
    {
        plumpCanvas.enabled = false;
    }

    private void ShowPlumpCanvas()
    {
        plumpCanvas.enabled = true;
    }

    public void ShowInteractionOpportunity()
    {
        interactionCanvas.enabled = true;
    }

    public void HideInteractionOpportunity()
    {
        interactionCanvas.enabled = false;
    }

    public void Interact()
    {
        if (Level1SceneManager.Instance.isHoldingPlump)
        {
            HideInteractionOpportunity();
            Level1SceneManager.Instance.isHoldingPlump = false;
            Level1SceneManager.Instance.isPlumping = true;
            ShowPlumpCanvas();
        }

    }

}
