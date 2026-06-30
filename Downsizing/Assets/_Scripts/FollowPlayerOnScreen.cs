using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerOnScreen : MonoBehaviour
{
    public GameObject playerToFollow;

    public Vector2 pixelOffset;

    private RectTransform rect;
    private Camera cam;
    private Canvas canvas;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        cam = Camera.main;
        canvas = FindFirstObjectByType<Canvas>();
    }

    public void Update()
    {
        if (!gameObject.activeSelf) return;
        Vector2 pos = cam.WorldToScreenPoint(playerToFollow.transform.position);
        pos.x += pixelOffset.x;
        pos.y += pixelOffset.y;
        rect.anchoredPosition = pos / canvas.scaleFactor;
        
    }
}
