using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limit : MonoBehaviour
{
    [SerializeField]
    private Crocodile crocodile;

    private Canvas talkCanvas = null;
    private bool hasTalked = false;

    private void Awake()
    {
        talkCanvas = gameObject.transform.GetChild(0).GetComponent<Canvas>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasTalked)
        {
            AudioSoundsManager.Instance.PlaySoundNoPassage();
            StartCoroutine(crocodile.Talk(talkCanvas));
        }
    }

    private void Start()
    {
        HideTalkCanvas();
    }

    public void HideTalkCanvas()
    {
        talkCanvas.enabled = false;
    }
}
