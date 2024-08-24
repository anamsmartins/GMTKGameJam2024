using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1SceneManager : MonoBehaviour
{
    public static Level1SceneManager Instance { get; private set; } = null;

    public bool completedPuzzle = false;
    public bool activatedLastInteraction = false;

    // Puzzle 1
    public bool hasTalkedToCrocodile = false;

    // Puzzle 2
    public bool isHoldingPlantKiller = false;
    public bool hasInteractedWTree = false;
    public bool isHoldingPlump = false;
    public bool isPlumping = false;
    public int plumpCount = 0;

    // Puzzle 3
    public bool finishedScalePuzzle = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
