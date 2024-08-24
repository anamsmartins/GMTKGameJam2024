using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.Windows;

public class Gardener : MonoBehaviour
{
    [Header ("Movement")]
    [SerializeField]
    private float walkSpeed = 7f;

    private FirstSceneInputActions inputs = null;
    private float horizontalDirection = 0;
    private float verticalDirection = 0;

    private Animator myAnimator = null;
    private Rigidbody2D myRigidBody = null;

    private Interaction myInteraction = null;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
        myInteraction = GetComponent<Interaction>();

        inputs = new FirstSceneInputActions();
        inputs.Gardener.Interact.performed += OnInteract;
        inputs.Gardener.Plump.performed += OnPlump;
        inputs.Gardener.Options.performed += OnOptions;
    }

    private void OnEnable() 
    {
        inputs.Enable();
    }

    private void Update()
    {
        horizontalDirection = inputs.Gardener.Move.ReadValue<Vector2>().x;
        verticalDirection = inputs.Gardener.Move.ReadValue<Vector2>().y;


        if (Level1SceneManager.Instance.activatedLastInteraction)
        {
            // Puzzle 1
            if (myAnimator.runtimeAnimatorController.name == "gardener")
            {
                myAnimator.SetBool("InteractWStairs", true);
                myRigidBody.velocity = Vector2.zero;
                UpdateGardenerAnimator(0, 0, false);
            }
            else if (myAnimator.runtimeAnimatorController.name == "gardenerLevel1Puzzle2")
            {
                //myRigidBody.velocity = Vector2.zero;
                //UpdateGardenerAnimator(0, 0, false);
                
                //myAnimator.SetTrigger("JumpOnMouse");
            }

        }

        // Puzzle 1
        if (Level1SceneManager.Instance.completedPuzzle)
        {
            myAnimator.SetBool("FinishedFirstPuzzle", true);
        }

        // Puzzle 2
        if (myAnimator.runtimeAnimatorController.name == "gardenerLevel1Puzzle2")
        {
            myAnimator.SetBool("HoldingPlantKiller", Level1SceneManager.Instance.isHoldingPlantKiller);

            if (Level1SceneManager.Instance.hasInteractedWTree && Level1SceneManager.Instance.isHoldingPlantKiller)
            {
                transform.right = Vector3.zero;
                myRigidBody.velocity = Vector2.zero;
                UpdateGardenerAnimator(0, 0, false);
                AudioSoundsManager.Instance.PlaySoundWeedKillerAnim();
                myAnimator.SetBool("PouringPlantKiller", true);
            }

            if (Level1SceneManager.Instance.isHoldingPlump) 
            { 
                myAnimator.SetBool("HoldingPlump", true);
            }
            else
            {
                if (myAnimator.GetBool("HoldingPlump")){
                    myAnimator.SetBool("HoldingPlump", false);
                }
            }

            // Plumpin
            if (Level1SceneManager.Instance.isPlumping)
            {
                transform.right = Vector3.zero;
                myRigidBody.velocity = Vector2.zero;
                UpdateGardenerAnimator(0, 0, false);
                if (!myAnimator.GetBool("Plumping"))
                {
                    transform.position = new Vector3(1.49f, -7.79f, 0);
                    myAnimator.SetBool("Plumping", true);
                }
            }
        }

        if (CanMove())
        {
            Move();
        }
    }

    private void OnDisable()
    {
        inputs.Disable();
    }

    private void Flip()
    {
        transform.right = -transform.right;
    }

    private void Move()
    {
        if(!AudioGrassManager.Instance.IsPlaying)
        {
            AudioGrassManager.Instance.PlaySoundWalking();
        }
        if (IsMoving())
        {
            if (IsMovingHorizontally())
            {
                if (ShouldFlip())
                {
                    Flip();
                }
                UpdateGardenerAnimator(Mathf.Abs(horizontalDirection), Mathf.Abs(0), false);
                myRigidBody.velocity = new Vector2(horizontalDirection * walkSpeed, 0);
            }
            else
            {
                UpdateGardenerAnimator(Mathf.Abs(0), Mathf.Abs(verticalDirection), verticalDirection > 0);
                myRigidBody.velocity = new Vector2(0, verticalDirection * walkSpeed);
            }
        }
        else
        {
            if (AudioGrassManager.Instance.IsPlaying)
            {
                AudioGrassManager.Instance.StopSoundWalking();
            }
            UpdateGardenerAnimator(Mathf.Abs(0), Mathf.Abs(0), false);
            myRigidBody.velocity = new Vector2(0, 0);
        }
    }

    private void UpdateGardenerAnimator(float horizontalDirection, float verticalDirection, bool isMovingUp)
    {
        myAnimator.SetFloat("HorizontalDirection", Mathf.Abs(horizontalDirection));
        myAnimator.SetFloat("VerticalDirection", Mathf.Abs(verticalDirection));
        myAnimator.SetBool("IsMovingUp", isMovingUp);
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        if (myInteraction.CanInteract()) 
        {
            myInteraction.Interact();
        }
    }

    private void OnPlump(InputAction.CallbackContext context)
    {
        if (Level1SceneManager.Instance.isPlumping && myAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name != "PlumpingDown")
        {
            AudioSoundsManager.Instance.PlaySoundPlump();
            myAnimator.SetTrigger("Plump");
            Level1SceneManager.Instance.plumpCount++;
        }
    }

    private void OnOptions(InputAction.CallbackContext context)
    {
        AudioSoundsManager.Instance.PlaySoundPressButton();
        UIManager.Instance.OnClickOptions();
    }

    private bool ShouldFlip()
    {
        return transform.right.x * horizontalDirection < 0;
    }

    private bool IsMoving()
    {
        return !(horizontalDirection == 0 && verticalDirection == 0);
    }

    private bool IsMovingHorizontally()
    {
        return Mathf.Abs(horizontalDirection) > Mathf.Abs(verticalDirection);
    }

    private bool CanMove() 
    {
        if (Level1SceneManager.Instance.activatedLastInteraction)
        {
            return false;
        }
        if (myAnimator.runtimeAnimatorController.name == "gardenerLevel1Puzzle2")
        {
            return !myAnimator.GetBool("PouringPlantKiller") && !Level1SceneManager.Instance.isPlumping;
        }
        return true;
    }

    public void MoveToNextLevel()
    {
        StartCoroutine(WaitABitForSound());
        
    }

    private IEnumerator WaitABitForSound()
    {
        AudioSoundsManager.Instance.PlaySoundLevelPass();
        yield return new WaitForSecondsRealtime(2f);
        LevelChanger.Instance.FadeToLevel("Next");
    }

    public void FinishedPouringPlantKiller()
    {
        Level1SceneManager.Instance.isHoldingPlantKiller = false;
        myAnimator.SetBool("PouringPlantKiller", false);
    }
    
    public void CheckIfFinishedPlumping()
    {
        StartCoroutine(WaitABitBeforChecking());
    }

    private IEnumerator WaitABitBeforChecking()
    {
        yield return new WaitForSecondsRealtime(0.1f);

        if (!Level1SceneManager.Instance.isPlumping)
        {
            Level1SceneManager.Instance.activatedLastInteraction = true;
            transform.position = new Vector3(7.64f, -7.79f, 0);
            myRigidBody.velocity = Vector2.zero;
            UpdateGardenerAnimator(0, 0, false);
            myAnimator.SetBool("Plumping", false);
        }
        else
        {
            myAnimator.SetTrigger("PlumpUp");
        }
    }
}
