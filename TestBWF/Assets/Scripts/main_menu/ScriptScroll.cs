using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptScroll : MonoBehaviour {

    [Range(1, 50)]
    [Header("Controllers")]
    public int panCount;
    [Range(0, 1500)]
    public int panOffset;
    [Range(0f, 20f)]
    public float snapSpeed;
    [Range(0f, 20f)]
    public float scaleSpeed;
    [Range(0f, 5f)]
    public float scaleOffset;
    [Header("Other Objects")]
    public GameObject panPrefab;
    public GameObject[] instPans;
    private Vector2[] pansPos;
    private Vector2[] panScale;
    private RectTransform contentRect;
    public static int selectedPanID;
    public static bool isScrolling;
    private Vector2 contentVector;
    public ScrollRect scrollRect;

    void Start()
    {
        contentRect = GetComponent<RectTransform>();
        // instPans = new GameObject[panCount];
        pansPos = new Vector2[panCount];
        panScale = new Vector2[panCount];

        for (int i = 0; i < panCount; i++)
        {            //instPans[i] = Instantiate(panPrefab, transform, false);
            if (i == 0) continue;
            instPans[i].transform.localPosition = new Vector2(instPans[i - 1].transform.localPosition.x + instPans[0].GetComponent<RectTransform>().sizeDelta.x + panOffset, instPans[i].transform.localPosition.y);
            pansPos[i] = -instPans[i].transform.localPosition;
        }

    }

    void FixedUpdate()
    {
        if (contentRect.anchoredPosition.x - 100 <= pansPos[0].x && !isScrolling || contentRect.anchoredPosition.x + 100 >= pansPos[pansPos.Length - 1].x && !isScrolling)
        {
            scrollRect.inertia = false;
        }

        float nearestPos = float.MaxValue;
        for (int i = 0; i < panCount; i++)
        {
            float distance = Mathf.Abs(contentRect.anchoredPosition.x - pansPos[i].x);
            if (distance < nearestPos)
            {
                nearestPos = distance;
                selectedPanID = i;
            }

            float scale = Mathf.Clamp(1 / (distance / (panOffset / 1.5f)) * scaleOffset, 0.5f, 1f);
            panScale[i].x = Mathf.SmoothStep(instPans[i].transform.localScale.x, scale, scaleSpeed * Time.fixedDeltaTime);
            panScale[i].y = Mathf.SmoothStep(instPans[i].transform.localScale.y, scale, scaleSpeed * Time.fixedDeltaTime);
            instPans[i].transform.localScale = panScale[i];
        }
        float scrollVelocity = Mathf.Abs(scrollRect.velocity.x);
        if (scrollVelocity < 400 && !isScrolling) scrollRect.inertia = false;
        if (scrollVelocity > 400 || isScrolling) return;

        if (isScrolling) return;
        contentVector.x = Mathf.SmoothStep(contentRect.anchoredPosition.x, pansPos[selectedPanID].x, snapSpeed * Time.fixedDeltaTime);
        contentRect.anchoredPosition = contentVector;
    }

    public void Drag(bool scroll)
    {
        isScrolling = scroll;
        if (scroll) scrollRect.inertia = true;
    }
}
