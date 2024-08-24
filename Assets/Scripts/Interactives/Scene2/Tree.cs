using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour, IInteractive
{
    [SerializeField] private Sprite[] smallTreeImages; 

    private Animator myAnimator = null;
    private Canvas interactionCanvas = null;
    private SpriteRenderer spriteRenderer = null;

    private bool hasKilledTree = false;
    private bool smallTreeWPump = false;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        interactionCanvas = gameObject.transform.GetChild(0).GetComponent<Canvas>();
    }

    private void Start()
    {
        HideInteractionOpportunity();
    }

    private void Update()
    {
        if (!hasKilledTree)
        {
           if (Level1SceneManager.Instance.hasInteractedWTree && !Level1SceneManager.Instance.isHoldingPlantKiller)
           {
                AudioSoundsManager.Instance.PlaySoundTree();
                myAnimator.SetBool("KillTree", true);
           }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((!Level1SceneManager.Instance.hasInteractedWTree && Level1SceneManager.Instance.isHoldingPlantKiller) || smallTreeWPump)
        {
            ShowInteractionOpportunity();
        }
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
        if (!Level1SceneManager.Instance.hasInteractedWTree && Level1SceneManager.Instance.isHoldingPlantKiller)
        {
            Level1SceneManager.Instance.hasInteractedWTree = true;
        }

        if(smallTreeWPump)
        {
            spriteRenderer.sprite = smallTreeImages[1];
            smallTreeWPump = false;
            Level1SceneManager.Instance.isHoldingPlump = true;
        }
        HideInteractionOpportunity();

    }

    public void GetSmall()
    {
        myAnimator.SetBool("KillTree", false);
        hasKilledTree = true;
        myAnimator.enabled = false;
        spriteRenderer.sprite = smallTreeImages[0];
        smallTreeWPump = true;
        ShowInteractionOpportunity();
    }
}
