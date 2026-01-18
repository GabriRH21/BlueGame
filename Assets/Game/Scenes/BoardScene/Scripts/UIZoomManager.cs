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
        float bankExtraScale = 2f;
       RectTransform canvasRect = zoomContainer.parent as RectTransform;

        Vector3 containerStartScale = zoomContainer.localScale;
        Vector3 containerEndScale = Vector3.one * zoomScale;

        Vector3 bankStartScale = target.localScale;
        Vector3 bankEndScale = bankStartScale * bankExtraScale;

        Vector2 containerStartPos = zoomContainer.anchoredPosition;

        Vector2 bankCanvasPos;
        Vector2 worldPos = target.TransformPoint(target.rect.center);

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            RectTransformUtility.WorldToScreenPoint(null, worldPos),
            null,
            out bankCanvasPos
        );

        Vector2 containerEndPos = -bankCanvasPos * zoomScale;

        float t = 0f;
        while (t < 1f) {
            t += Time.deltaTime / duration;

            zoomContainer.localScale = Vector3.Lerp(containerStartScale, containerEndScale, t);
            zoomContainer.anchoredPosition = Vector2.Lerp(containerStartPos, containerEndPos, t);

            target.localScale = Vector3.Lerp(bankStartScale, bankEndScale, t);

            yield return null;
        }

        zoomContainer.localScale = containerEndScale;
        zoomContainer.anchoredPosition = containerEndPos;
        target.localScale = bankEndScale;
    }
}
