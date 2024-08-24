using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRake : MonoBehaviour, IInteractive
{
    [SerializeField] private Crocodile crocodile;

    private Canvas interactionCanvas = null;
    private Canvas talkCanvas = null;
    private bool isTalking = false;

    private void Awake()
    {
        interactionCanvas = gameObject.transform.GetChild(0).GetComponent<Canvas>();
        talkCanvas = gameObject.transform.GetChild(1).GetComponent<Canvas>();
    }

    private void Start()
    {
        HideInteractionOpportunity();
        HideTalkCanvas();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isTalking)
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
        if (!isTalking) { 
            HideInteractionOpportunity();
            AudioSoundsManager.Instance.PlaySoundDontTouchSleepingRat();
            StartCoroutine(crocodile.Talk(talkCanvas));
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

    public void HideTalkCanvas()
    {
        talkCanvas.enabled = false;
    }
}
