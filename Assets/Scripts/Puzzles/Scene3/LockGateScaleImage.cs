using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LockGateScaleImage : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{

    [HideInInspector] public Transform parentAfterDrag;
    private Image image;
    private RectTransform rectTransform;

    private Vector2 initialSizeDelta = new Vector2();
    private Vector2 OnSwipeStart = new Vector2();
    private Vector2 OnSwipeEnd = new Vector2();
    private Vector2 OnSwipe = new Vector2();

    private Vector2 finishSize = new Vector2(538.022f, 538.0148f);

    private bool completedScalePuzzle = false;

    private void Start()
    {
        image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!completedScalePuzzle) { 
            OnSwipeStart = eventData.position;
            initialSizeDelta = rectTransform.sizeDelta;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!completedScalePuzzle)
        {
            OnSwipe = eventData.position - OnSwipeStart;

            rectTransform.sizeDelta = new Vector2(Mathf.Min(initialSizeDelta.x + Mathf.Abs(OnSwipe.x), initialSizeDelta.x + 362), Mathf.Min(initialSizeDelta.y + Mathf.Abs(OnSwipe.y), initialSizeDelta.y + 362));
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!completedScalePuzzle)
        {
            //transform.SetParent(parentAfterDrag);
            //image.raycastTarget = true;
            if (rectTransform.sizeDelta.x >= finishSize.x && rectTransform.sizeDelta.y >= finishSize.y)
            {
                rectTransform.sizeDelta = finishSize;
                completedScalePuzzle = true;
                Level1SceneManager.Instance.finishedScalePuzzle = true;
                StartCoroutine(WaitABitSound());
            } else
            {
                rectTransform.sizeDelta = initialSizeDelta;
            }
        }
        
    }

    private IEnumerator WaitABitSound()
    {
        yield return new WaitForSecondsRealtime(1f);
        AudioSoundsManager.Instance.PlaySoundLockUnlock();
    }
}
