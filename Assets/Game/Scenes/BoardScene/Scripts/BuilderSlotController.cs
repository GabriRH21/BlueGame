using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuilderSlotController : MonoBehaviour
{
    private PieceController _piece;
    [SerializeField] private Button _button;
    [SerializeField] private BuilderSlotController[] _slots;

    private void Awake() {
        _button.onClick.AddListener(SlotSelected);
    }

    private void SlotSelected() {
        if (Globals.PlayerStats.PiecesInHand.Count == 0) {
            return;
        }

        
    }
}
