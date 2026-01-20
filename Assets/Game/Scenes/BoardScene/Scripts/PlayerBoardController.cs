using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine;

public class PlayerBoardController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private const float MOVEMENT_DISTANCE = 372;
    private Vector2 initialPos;

    private void Awake() {
        initialPos = this.transform.position;
        StartCoroutine(Hide(0));
    }

    public void OnPointerEnter(PointerEventData p) {
        StartCoroutine(Show());
    }

    public void OnPointerExit(PointerEventData p) {
        StartCoroutine(Hide());
    }

    private IEnumerator Show(float duration = 0.5f) {
        Vector3 startPos = this.transform.localPosition;
        Vector3 endPos = startPos + new Vector3(0, MOVEMENT_DISTANCE, 0);
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            this.transform.localPosition = Vector3.Lerp(startPos, endPos, elapsed / duration);
            yield return null;
        }

        this.transform.localPosition = endPos; 
    }
    
    private IEnumerator Hide(float duration = 0.5f) {
        //yield return GoUp();
        Vector3 startPos = this.transform.localPosition;
        Vector3 endPos = startPos - new Vector3(0, MOVEMENT_DISTANCE, 0);
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
