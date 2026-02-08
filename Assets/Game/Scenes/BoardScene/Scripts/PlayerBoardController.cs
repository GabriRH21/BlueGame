using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.UI;

public class PlayerBoardController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private PenaltySlotController[] _penaltySlots;
    private const float MOVEMENT_DISTANCE = 372;
    private Vector2 initialPos;
    private bool isMoving = false;

    private void Awake() {
        BoardEventManager.AddPenalty += AddPenalty;
        BoardEventManager.AddCenterPenalty += AddCenterPenalty;
        initialPos = this.transform.position;
        StartCoroutine(ShowHide(this.transform.localPosition - new Vector3(0, MOVEMENT_DISTANCE, 0), 0f));
    }

    public void OnPointerEnter(PointerEventData p) {
        if (!isMoving) {
            isMoving = true;
            StartCoroutine(ShowHide(this.transform.localPosition + new Vector3(0, MOVEMENT_DISTANCE, 0)));
        }
    }

    public void OnPointerExit(PointerEventData p) {
        if (!isMoving) {
            isMoving = true;
            StartCoroutine(ShowHide(this.transform.localPosition - new Vector3(0, MOVEMENT_DISTANCE, 0)));
        }
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
        isMoving = false;
    }

    private void AddCenterPenalty() {
        int index = 0;
        bool done = false;
        do {
            if (!_penaltySlots[index].HasPenalty()) {
                _penaltySlots[index].ConfirmPenalty();
                done = true;
            }
            index++;
        } while (index < _penaltySlots.Length && !done);
    }

    private void AddPenalty(int quantity, PieceController piece) {
        int index = 0;
        do {
            if (!_penaltySlots[index].HasPenalty()) {
                _penaltySlots[index].ConfirmPenalty(piece);
                quantity--;
            }
            index++;
        } while (quantity != 0 && index < _penaltySlots.Length);
    }
}
