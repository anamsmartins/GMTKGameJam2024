using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateLock : MonoBehaviour, IInteractive
{
    private Canvas interactionCanvas = null;

    private void Awake()
    {
        interactionCanvas = gameObject.transform.GetChild(0).GetComponent<Canvas>();
    }

    private void Start()
    {
        HideInteractionOpportunity();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ShowInteractionOpportunity();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        HideInteractionOpportunity();
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
        AudioSoundsManager.Instance.PlaySoundTouchLock();
        HideInteractionOpportunity();
        UIManager.Instance.OnClickPuzzle();
    }
}
