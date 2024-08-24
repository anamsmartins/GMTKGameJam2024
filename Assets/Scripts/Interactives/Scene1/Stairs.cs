using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : MonoBehaviour, IInteractive
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
        if (Level1SceneManager.Instance.completedPuzzle)
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
        if (Level1SceneManager.Instance.completedPuzzle)
        {
            Level1SceneManager.Instance.activatedLastInteraction = true;
            HideInteractionOpportunity();
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
}
