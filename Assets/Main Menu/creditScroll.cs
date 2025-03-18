using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditScroll : MonoBehaviour
{
    public float scrollSpeed = 200f;
    public RectTransform rectTransform;
    public ScrollRect scrollView;
    public float startY;
    public float endY;
    public bool autoScroll = true;
    public float manualScrollSpeed = 10f; 

    void Start()
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
        rectTransform = GetComponent<RectTransform>();
        startY = rectTransform.anchoredPosition.y;
        endY = rectTransform.sizeDelta.y;
        scrollView.verticalNormalizedPosition = 1;
        scrollView.vertical = false; 
        scrollView.scrollSensitivity = manualScrollSpeed; 
    }

    void Update()
    {
        if (autoScroll)
        {
            if (rectTransform.anchoredPosition.y <= endY)
            {
                rectTransform.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;
            }
            else
            {
                autoScroll = false;
                scrollView.vertical = true; 
            }
        }
    }

    public void ResetScroll()
    {
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, startY);
        autoScroll = true;
        scrollView.vertical = false; 
    }
}