using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class RatPuzzleSlot : MonoBehaviour, IDropHandler
{

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        Rat rat = dropped.GetComponent<Rat>();

        if (gameObject.name == "Rat2PuzzleSlot" && rat.gameObject.name == "Rat2")
        {
            rat.parentAfterDrag = transform;
            StartCoroutine(WaitABit(rat));

            return;
        }

        if (gameObject.name == "Rat3PuzzleSlot" && rat.gameObject.name == "Rat3")
        {
            rat.parentAfterDrag = transform;
            StartCoroutine(WaitABit(rat));
            return;
        }
    }


    IEnumerator WaitABit(Rat rat)
    {
        yield return new WaitForSecondsRealtime(0.5f);
        rat.hasBeenDropped = true;
    }
}
