using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIZoomManager : MonoBehaviour
{
    public RectTransform zoomContainer;
    public float zoomScale = 1.5f;
    public float duration = 0.5f;

    private Vector3 originalScale;
    private Vector2 originalPos;

    void Start()
    {
        originalScale = zoomContainer.localScale;
        originalPos = zoomContainer.anchoredPosition;
    }

    public void ZoomTo(RectTransform target)
    {
        StopAllCoroutines();
        StartCoroutine(ZoomCoroutine(target, zoomScale));
    }

    public void ResetZoom()
    {
        StopAllCoroutines();
        StartCoroutine(ZoomCoroutine(null, 3f, true));
    }

    IEnumerator ZoomCoroutine(RectTransform target, float targetScale, bool zoomOut=false)
    {
        RectTransform canvasRect = zoomContainer.parent as RectTransform;

    Vector3 startScale = zoomContainer.localScale;
    Vector3 endScale = Vector3.one * zoomScale;

    Vector2 startPos = zoomContainer.anchoredPosition;

    // Posición real del bank en canvas
    Vector2 bankCanvasPos;
    Vector2 worldPos = target.TransformPoint(target.rect.center);

    RectTransformUtility.ScreenPointToLocalPointInRectangle(
        canvasRect,
        RectTransformUtility.WorldToScreenPoint(null, worldPos),
        null,
        out bankCanvasPos
    );

    Vector2 endPos = -bankCanvasPos * zoomScale;

    float t = 0f;
    while (t < 1f)
    {
        t += Time.deltaTime / duration;
        zoomContainer.localScale = Vector3.Lerp(startScale, endScale, t);
        zoomContainer.anchoredPosition = Vector2.Lerp(startPos, endPos, t);
        yield return null;
    }

    zoomContainer.localScale = endScale;
    zoomContainer.anchoredPosition = endPos;
    }
}
