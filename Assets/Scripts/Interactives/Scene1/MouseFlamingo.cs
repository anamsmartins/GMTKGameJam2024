using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFlamingo : MonoBehaviour, IInteractive
{
    [SerializeField] private Sprite flamingoWithoutMouse;
    [SerializeField] private Rat rat2;


    private Canvas interactionCanvas = null;
    private Canvas talkCanvas = null;

    private SpriteRenderer spriteRenderer = null;

    private bool alreadyInteracted = false;

    private void Awake()
    {
        interactionCanvas = gameObject.transform.GetChild(0).GetComponent<Canvas>();
        talkCanvas = gameObject.transform.GetChild(1).GetComponent<Canvas>();

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        HideInteractionOpportunity();
        HideTalkCanvas();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!alreadyInteracted && Level1SceneManager.Instance.hasTalkedToCrocodile)
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
        if (!alreadyInteracted && Level1SceneManager.Instance.hasTalkedToCrocodile)
        {
            AudioSoundsManager.Instance.PlaySoundMouseSqueak();
            HideInteractionOpportunity();
            alreadyInteracted = true;
            StartCoroutine(Catch());
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

    public void ShowTalkCanvas()
    {
        talkCanvas.enabled = true;
    }

    public void HideTalkCanvas()
    {
        talkCanvas.enabled = false;
    }

    IEnumerator Catch()
    {
        ShowTalkCanvas();
        spriteRenderer.sprite = flamingoWithoutMouse;
        rat2.Catch();

        yield return new WaitForSecondsRealtime(1);

        HideTalkCanvas();
    }
}