using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crocodile : MonoBehaviour, IInteractive
{
    private Animator myAnimator;

    private Canvas interactionCanvas = null;
    private Canvas talkCanvas = null;

    private bool isTalking = false;
    private bool firstTimeInteract = true;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
        interactionCanvas = gameObject.transform.GetChild(0).GetComponent<Canvas>();
        talkCanvas = gameObject.transform.GetChild(1).GetComponent<Canvas>();

    }

    private void Start()
    {
        HideInteractionOpportunity();
        HideTalkCanvas(talkCanvas);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isTalking && !Level1SceneManager.Instance.completedPuzzle)
        {
            ShowInteractionOpportunity();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        HideInteractionOpportunity();
    }

    public void Interact()
    {
        if (!isTalking)
        {
            HideInteractionOpportunity();

            if (firstTimeInteract)
            {
                firstTimeInteract = false;
                Level1SceneManager.Instance.hasTalkedToCrocodile = true;
                AudioSoundsManager.Instance.PlaySoundCrocodileJoke();
                StartCoroutine(Talk(talkCanvas));
                StartCoroutine(ShowPuzzleMenu());
                return;
            }

            UIManager.Instance.OnClickPuzzle();
        }
    }

    public void ShowInteractionOpportunity()
    {
        interactionCanvas.enabled = true;
    }

    public void HideInteractionOpportunity()
    {
        interactionCanvas.enabled = false;
    }

    public void ShowTalkCanvas(Canvas canvas)
    {
        canvas.enabled = true;
    }

    public void HideTalkCanvas(Canvas canvas)
    {
        canvas.enabled = false;
    }

    public IEnumerator Talk(Canvas canvas)
    {
        isTalking = true;
        myAnimator.SetBool("IsTalking", true);
        ShowTalkCanvas(canvas);

        yield return new WaitForSecondsRealtime(2);

        HideTalkCanvas(canvas);
        myAnimator.SetBool("IsTalking", false);
        isTalking = false;
    }

    IEnumerator ShowPuzzleMenu()
    {
        yield return new WaitForSecondsRealtime(2);

        UIManager.Instance.OnClickPuzzle();
    }

}

