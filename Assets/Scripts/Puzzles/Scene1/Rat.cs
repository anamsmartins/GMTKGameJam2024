using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Rat : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Image image;

    [SerializeField] private Sprite[] images;

    [HideInInspector] public Transform parentAfterDrag;
    [HideInInspector] public bool hasBeenDropped = false;
    [HideInInspector] public bool hasBeenCaught = false;

    private RectTransform rectTransform;

    private Vector2 initialSizeDelta = new Vector2();
    private Vector2 initialPivot = new Vector2();
    private Vector2 OnSwipeStart = new Vector2();
    private Vector2 OnSwipeEnd = new Vector2();
    private Vector2 OnSwipe = new Vector2();
    private string previousDirection = "";

    private int currentImageIndex = -1;
    private bool onePositionDown = false;

    private void Start()
    {   
        rectTransform = GetComponent<RectTransform>();

        //DefineMaxScaleFactors();

        if (CanBeDragged() && !hasBeenCaught)
        {
            gameObject.SetActive(false);
        }
        else
        {
            if (!CanBeDragged())
            {
                hasBeenDropped = true;
                rectTransform.anchoredPosition = new Vector2(-2, -53);
            }
            gameObject.SetActive(true);
        }
        
        currentImageIndex = -1;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!hasBeenDropped && CanBeDragged())
        {
            AudioSoundsManager.Instance.PlaySoundPickRat();
            PickUpRat();
        } else if (hasBeenDropped)
        {
            // Save first position
            OnSwipeStart = eventData.position;
            initialSizeDelta = rectTransform.sizeDelta;
            initialPivot = rectTransform.pivot;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!hasBeenDropped && CanBeDragged())
        {
            DragRat();
        }
        else if (hasBeenDropped)
        {
            // Stretch current image according to direction wasd
            OnSwipe = eventData.position - OnSwipeStart;

            // Determine direction to stretch
            if (Mathf.Abs(OnSwipe.y) > Mathf.Abs(OnSwipe.x))
            {
                // Vertical
                if (OnSwipe.y > 0)
                {
                    // Up
                    if (RatManager.Instance.GetRatMaxScaleValue(gameObject.name, 2) >=100)
                    {
                        if (previousDirection != "Up")
                        {
                            rectTransform.sizeDelta = initialSizeDelta;
                            previousDirection = "Up";
                        }

                        Vector2 newPivot = new Vector2(initialPivot.x, 0);
                        Vector2 newAnch = GetNewAnchor(newPivot);

                        rectTransform.pivot = newPivot;
                        rectTransform.anchoredPosition = newAnch;

                        rectTransform.sizeDelta = new Vector2(initialSizeDelta.x, Mathf.Min(initialSizeDelta.y + Mathf.Abs(OnSwipe.y), initialSizeDelta.y + RatManager.Instance.GetRatMaxScaleValue(gameObject.name, 2)));
                    }

                }
                else
                {
                    // Down
                    if (RatManager.Instance.GetRatMaxScaleValue(gameObject.name, 3) >= 100)
                    {
                        if (previousDirection != "Down")
                        {
                            rectTransform.sizeDelta = initialSizeDelta;
                            previousDirection = "Down";
                        }

                        Vector2 newPivot = new Vector2(initialPivot.x, 1);
                        Vector2 newAnch = GetNewAnchor(newPivot);

                        rectTransform.pivot = newPivot;
                        rectTransform.anchoredPosition = newAnch;

                        rectTransform.sizeDelta = new Vector2(initialSizeDelta.x, Mathf.Min(initialSizeDelta.y + Mathf.Abs(OnSwipe.y), initialSizeDelta.y + RatManager.Instance.GetRatMaxScaleValue(gameObject.name, 3)));
                    }
                }
            }
            else
            {
                // Horizontal
                if (OnSwipe.x > 0)
                {
                    // Right
                    if (RatManager.Instance.GetRatMaxScaleValue(gameObject.name, 1) >= 100)
                    {
                        if (previousDirection != "Right")
                        {
                            rectTransform.sizeDelta = initialSizeDelta;
                            previousDirection = "Right";
                        }

                        Vector2 newPivot = new Vector2(0, initialPivot.y);
                        Vector2 newAnch = GetNewAnchor(newPivot);

                        rectTransform.pivot = newPivot;
                        rectTransform.anchoredPosition = newAnch;

                        rectTransform.sizeDelta = new Vector2(Mathf.Min(initialSizeDelta.x + Mathf.Abs(OnSwipe.x), initialSizeDelta.x + RatManager.Instance.GetRatMaxScaleValue(gameObject.name, 1)), initialSizeDelta.y);
                    }
                }
                else
                {
                    // Left
                    if (RatManager.Instance.GetRatMaxScaleValue(gameObject.name, 0) >= 100)
                    {
                        if (previousDirection != "Left")
                        {
                            rectTransform.sizeDelta = initialSizeDelta;
                            previousDirection = "Left";
                        }

                        Vector2 newPivot = new Vector2(1, initialPivot.y);
                        Vector2 newAnch = GetNewAnchor(newPivot);

                        rectTransform.pivot = newPivot;
                        rectTransform.anchoredPosition = newAnch;

                        rectTransform.sizeDelta = new Vector2(Mathf.Min(initialSizeDelta.x - Mathf.Abs(OnSwipe.x), initialSizeDelta.x + RatManager.Instance.GetRatMaxScaleValue(gameObject.name, 0)), initialSizeDelta.y);
                    }
                }
            }

        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!hasBeenDropped && CanBeDragged())
        {
            AudioSoundsManager.Instance.PlaySoundPutRat();
            PlaceRat();
        }
        else if (hasBeenDropped)
        {
            // Update image according to size of strecht
            // Get current mouse position
            OnSwipeEnd = eventData.position;

            // Calculate the stretched difference and direction
            Vector2 difference = OnSwipeEnd - OnSwipeStart;
            if (Mathf.Abs(difference.y) > Mathf.Abs(difference.x))
            {
                if(OnSwipeEnd.y > OnSwipeStart.y)
                {
                    // Up
                    UpdateRatImage("Up", difference);

                }
                else
                {
                    // Down
                    UpdateRatImage("Down", difference);
                }
            }
            else
            {
                if (OnSwipeEnd.x > OnSwipeStart.x)
                {
                    // Right
                    UpdateRatImage("Right", difference);

                }
                else
                {
                    // Left
                    UpdateRatImage("Left", difference);
                }
            }
        }
    }

    private void PickUpRat()
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.parent.parent);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }

    private void DragRat()
    {
        transform.position = Input.mousePosition;
    }

    private void PlaceRat()
    {
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
        if (parentAfterDrag.gameObject.name == "Rat2PuzzleSlot")
        {
            hasBeenDropped = true;
            StartCoroutine(WaitABit());
        } else if  (parentAfterDrag.gameObject.name == "Rat3PuzzleSlot")
        {
            hasBeenDropped = true;
            StartCoroutine(WaitABit());
        }
    }

    

    private void UpdateRatImage(string direction, Vector2 difference)
    {
        int differenceRounded = 0;
        switch (gameObject.name)
        {
            case "Rat1":
                switch (direction)
                {
                    case "Up":
                        if (difference.y >= 100 && difference.y < 200)
                        {
                            // Grow 1
                            currentImageIndex += 1;
                            differenceRounded = 100;
                        } else if (difference.y >= 200 && difference.y < 300)
                        {
                            // Grow 2
                            currentImageIndex += 2;
                            differenceRounded = 200;
                        }
                        else if (difference.y >= 300)
                        {
                            // Grow 3
                            currentImageIndex += 3;
                            differenceRounded = 300;
                        }
                        if (RatManager.Instance.GetRatMaxScaleValue(gameObject.name, 2) >= 100)
                        {
                            if (differenceRounded != 0)
                            {
                                rectTransform.sizeDelta = new Vector2(100, (currentImageIndex + 3) * 100);
                                initialSizeDelta = rectTransform.sizeDelta;
                                float newValue = RatManager.Instance.GetRatMaxScaleValue(gameObject.name, 2) - differenceRounded;
                                RatManager.Instance.SetRatMaxScaleValues(gameObject.name, 2, newValue);
                                image.sprite = images[currentImageIndex];
                            }
                            else
                            {
                                rectTransform.sizeDelta = initialSizeDelta;
                            }
                        }
                        break;
                    case "Down":
                        if (Mathf.Abs(difference.y) >= 100 && Mathf.Abs(difference.y) < 200)
                        {
                            // Grow 1
                            currentImageIndex += 1;
                            differenceRounded = 100;
                        }
                        else if (Mathf.Abs(difference.y) >= 200 && Mathf.Abs(difference.y) < 300)
                        {
                            // Grow 2
                            currentImageIndex += 2;
                            differenceRounded = 200;
                        }
                        if (RatManager.Instance.GetRatMaxScaleValue(gameObject.name, 3) >= 100)
                        {
                            if (differenceRounded != 0)
                            {
                                rectTransform.sizeDelta = new Vector2(100, (currentImageIndex + 3) * 100);
                                initialSizeDelta = rectTransform.sizeDelta;
                                float newValue = RatManager.Instance.GetRatMaxScaleValue(gameObject.name, 3) - differenceRounded;
                                RatManager.Instance.SetRatMaxScaleValues(gameObject.name, 3, newValue);
                                image.sprite = images[currentImageIndex];
                            } else
                            {
                                rectTransform.sizeDelta = initialSizeDelta;
                            }
                        }
                        break;
                    default:
                        break;
                }
                break;
            case "Rat2":
                switch (direction)
                {
                    case "Up":
                        if (difference.y >= 100 && difference.y < 200)
                        {
                            // Grow 1
                            currentImageIndex += 1;
                            differenceRounded = 100;
                        }
                        if (RatManager.Instance.GetRatMaxScaleValue(gameObject.name, 2) >= 100)
                        {
                            if (differenceRounded != 0)
                            {
                                float newValue = RatManager.Instance.GetRatMaxScaleValue(gameObject.name, 2) - differenceRounded;
                                RatManager.Instance.SetRatMaxScaleValues(gameObject.name, 2, newValue);
                                image.sprite = images[currentImageIndex];
                            }
                        }
                        break;
                    default:
                        break;
                }
                rectTransform.sizeDelta = new Vector2(200, 300);
                break;
            case "Rat3":
                switch (direction)
                {
                    case "Up":
                        if (difference.y >= 100 && difference.y < 200)
                        {
                            // Grow 1
                            differenceRounded = 100;
                        }
                        if (RatManager.Instance.GetRatMaxScaleValue(gameObject.name, 2) >= 100)
                        {
                            if (differenceRounded != 0)
                            {
                                if (!onePositionDown)
                                {
                                    image.sprite = images[1];
                                    onePositionDown = true;
                                }
                                else
                                {
                                    image.sprite = images[2];
                                }

                                float newValue = RatManager.Instance.GetRatMaxScaleValue(gameObject.name, 2) - differenceRounded;
                                RatManager.Instance.SetRatMaxScaleValues(gameObject.name, 2, newValue);
                            }
                        }
                        break;
                    case "Right":
                        if (difference.x > 200)
                        {
                            // Grow 1
                            differenceRounded = 100;
                        }
                        if (RatManager.Instance.GetRatMaxScaleValue(gameObject.name, 1) >= 100)
                        {

                            if (differenceRounded != 0)
                            {
                                if (!onePositionDown)
                                {

                                    image.sprite = images[0];
                                    onePositionDown = true;
                                }
                                else
                                {
                                    image.sprite = images[2];
                                }
                                float newValue = RatManager.Instance.GetRatMaxScaleValue(gameObject.name, 1) - differenceRounded;
                                RatManager.Instance.SetRatMaxScaleValues(gameObject.name, 1,  newValue);
                            }
                        }

                        break;
                    default:
                        break;
                }
                rectTransform.sizeDelta = new Vector2(200, 400);

                break;
            default:
                break;
        }

        if (RatManager.Instance.HasCompletedPuzzle())
        {
            StartCoroutine(WaitABitBeforeClose());
        }
    }

    private void RemoveGridLayoutComponent()
    {
        Transform parentTransform = transform.parent;

        if (parentTransform != null)
        {
            Component component = parentTransform.GetComponent("GridLayoutGroup");

            if (component != null)
            {
                Destroy(component);
            }
            if (gameObject.name == "Rat2")
            {
                parentTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(-679, -241);

                rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
                rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
                rectTransform.pivot = new Vector2(0.5f, 0.5f);
                rectTransform.anchoredPosition = new Vector2(0, 0);
                rectTransform.sizeDelta = new Vector2(200, 300);

                initialSizeDelta = rectTransform.sizeDelta;
                initialPivot = rectTransform.pivot;
            }
            else if (gameObject.name == "Rat3")
            {
                parentTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(-679, 179.5f);

                rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
                rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
                rectTransform.pivot = new Vector2(0.5f, 0.5f);
                rectTransform.anchoredPosition = new Vector2(0, 0);
                rectTransform.sizeDelta = new Vector2(200, 400);

                initialSizeDelta = rectTransform.sizeDelta;
                initialPivot = rectTransform.pivot;
            }

        }
    }

    private Vector2 GetNewAnchor(Vector2 newPivot)
    {
        Vector3 op = new Vector3(rectTransform.rect.width * newPivot.x - rectTransform.rect.width * rectTransform.pivot.x, rectTransform.rect.height * newPivot.y - rectTransform.rect.height * rectTransform.pivot.y, 0);
        Vector3 pt = rectTransform.TransformPoint(op);
        return rectTransform.parent.InverseTransformPoint(pt);
    }

    private bool CanBeDragged() 
    {
        return gameObject.name != "Rat1";
    }

    private IEnumerator WaitABit()
    {
        yield return new WaitForSecondsRealtime(0.5f);

        RemoveGridLayoutComponent();
    }
    private IEnumerator WaitABitBeforeClose()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        Level1SceneManager.Instance.completedPuzzle = true;
    }

    public void Catch()
    {
        gameObject.SetActive(true);
        hasBeenCaught = true;
    }

}
