using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine;

public class PlayerBoardController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private const float MOVEMENT_DISTANCE = 372;
    private Vector2 initialPos;

    private void Awake() {
        initialPos = this.transform.position;
        StartCoroutine(ShowHide(this.transform.localPosition - new Vector3(0, MOVEMENT_DISTANCE, 0), 0f));
    }

    public void OnPointerEnter(PointerEventData p) {
        StartCoroutine(ShowHide(this.transform.localPosition + new Vector3(0, MOVEMENT_DISTANCE, 0)));
    }

    public void OnPointerExit(PointerEventData p) {
        StartCoroutine(ShowHide(this.transform.localPosition - new Vector3(0, MOVEMENT_DISTANCE, 0)));
    }

    private IEnumerator ShowHide(Vector3 endPos, float duration = 0.5f) {
        Vector3 startPos = this.transform.localPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            this.transform.localPosition = Vector3.Lerp(startPos, endPos, elapsed / duration);
            yield return null;
        }

        this.transform.localPosition = endPos; 
    }
}
