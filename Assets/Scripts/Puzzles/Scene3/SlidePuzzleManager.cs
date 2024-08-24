using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SlidePuzzleManager : MonoBehaviour
{
    public static SlidePuzzleManager Instance { get; private set; } = null;

    [SerializeField] private Canvas puzzleSlideCanvas;
    [SerializeField] private Canvas scaleCanvas;

    private Dictionary<string, List<int>> directionPossiblePositions = new Dictionary<string, List<int>>();

    private bool hasFinishedSlidePuzzle = false;

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

    private void Start()
    {
        DefinePossiblePositionsEachDirection();
    }

    private void Update()
    {
        if (hasFinishedSlidePuzzle)
        {
            StartCoroutine(WaitABit());
        }
    }

    private void DefinePossiblePositionsEachDirection()
    {
        List<int> leftPositions = new List<int>()
        {
            0, 1, 3, 4, 6, 7
        };
        List<int> rightPositions = new List<int>()
        {
            1, 2, 4, 5, 7, 8
        };
        List<int> upPositions = new List<int>()
        {
            0, 1, 2, 3, 4, 5
        };
        List<int> downPositions = new List<int>()
        {
            3, 4, 5, 6, 7, 8
        };
        directionPossiblePositions.Add("Left", leftPositions);
        directionPossiblePositions.Add("Right", rightPositions);
        directionPossiblePositions.Add("Up", upPositions);
        directionPossiblePositions.Add("Down", downPositions);

    }

    public void OnClickPuzzlePiece(GameObject piece)
    {
        AudioSoundsManager.Instance.PlaySoundMovePuzzlePiece();
        if (!hasFinishedSlidePuzzle)
        {
            string pieceName = piece.name;

            // Current position in canvas
            int piecePosition = piece.transform.GetSiblingIndex();

            int piece9Position;

            piece9Position = CheckIfEmptyPieceNear("Left", piecePosition);
            if (piece9Position != -1)
            {
                piece.transform.SetSiblingIndex(piece9Position);
                hasFinishedSlidePuzzle = IsWinningCondition();
                return;
            }

            piece9Position = CheckIfEmptyPieceNear("Right", piecePosition);
            if (piece9Position != -1)
            {
                piece.transform.SetSiblingIndex(piece9Position);
                hasFinishedSlidePuzzle = IsWinningCondition();
                return;
            }

            piece9Position = CheckIfEmptyPieceNear("Up", piecePosition);
            if (piece9Position != -1)
            {
                int previousPiecePosition = piecePosition;
                piece.transform.SetSiblingIndex(piece9Position);
                puzzleSlideCanvas.GetComponentsInChildren<Transform>()[piece9Position + 2].SetSiblingIndex(previousPiecePosition);
                hasFinishedSlidePuzzle = IsWinningCondition();
                return;
            }

            piece9Position = CheckIfEmptyPieceNear("Down", piecePosition);

            if (piece9Position != -1)
            {
                // Trade positions
                int previousPiecePosition = piecePosition;
                piece.transform.SetSiblingIndex(piece9Position);
                puzzleSlideCanvas.GetComponentsInChildren<Transform>()[piece9Position].SetSiblingIndex(previousPiecePosition);
                hasFinishedSlidePuzzle = IsWinningCondition();
                return;
            }
        }

    }

    private int CheckIfEmptyPieceNear(string direction, int piecePosition)
    {
        int count;
        switch (direction)
        {
            case "Left":
                count = piecePosition - 1;
                if (directionPossiblePositions["Left"].Contains(count))
                {
                    if(puzzleSlideCanvas.GetComponentsInChildren<Image>()[count].name == "Piece9")
                    {
                        return count;
                    }
                }
                break;
            case "Right":
                count = piecePosition + 1;
                
                if (directionPossiblePositions["Right"].Contains(count))
                {
                    if (puzzleSlideCanvas.GetComponentsInChildren<Image>()[count].name == "Piece9")
                    {
                        return count;
                    }
                }
                break;
            case "Up":
                count = piecePosition - 3;
                if (directionPossiblePositions["Up"].Contains(count))
                {
                    if (puzzleSlideCanvas.GetComponentsInChildren<Image>()[count].name == "Piece9")
                    {
                        return count;
                    }
                }
                break;
            case "Down":
                count = piecePosition + 3;
                if (directionPossiblePositions["Down"].Contains(count))
                {
                    if (puzzleSlideCanvas.GetComponentsInChildren<Image>()[count].name == "Piece9")
                    {
                        return count;
                    }
                }
                break;
            default:
                break;
        }

        return -1;
    }

    private bool IsWinningCondition()
    {
        int index = 1;
        foreach (Image puzzlePiece in puzzleSlideCanvas.GetComponentsInChildren<Image>())
        {

            if (!puzzlePiece.name.EndsWith(index.ToString()))
            {
                return false;
            }
            index++;
        }

        return true;
    }

    private IEnumerator WaitABit()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 1;
        scaleCanvas.gameObject.SetActive(true);
        puzzleSlideCanvas.gameObject.SetActive(false);
    }
}
