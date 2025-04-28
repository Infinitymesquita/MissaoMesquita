using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SortStringsFeedbackUI : MonoBehaviour
{
    [Multiline]
    [SerializeField] string[] linesFeedback;
    [SerializeField] TextMeshProUGUI textFeedback;
    [SerializeField] GameObject feedbackUIGameObject;    
    public void SortLines()
    {
        feedbackUIGameObject.SetActive(true);
        int indexLine = Random.Range(0, linesFeedback.Length);
        textFeedback.text = linesFeedback[indexLine];
    }

}
