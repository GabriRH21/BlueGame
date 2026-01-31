using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuilderSlotController : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private SlotController[] _slots;

    private void Awake() {
        _button.onClick.AddListener(SlotSelected);
    }

    private void SlotSelected() {
        int numberOfPieces = Globals.PlayerStats.PiecesInHand.Count;
        if (numberOfPieces == 0) {
            return;
        }
        
        if (IsBuildingAllRow()) {
            return;
        }
        
        PieceController piece = Globals.PlayerStats.PiecesInHand[0];
        if (!AreBuildingThisPiece(piece)) {
            return;
        }
        
        foreach (var slot in _slots) {
            if (!slot.IsBuilding()) {
                slot.Build(piece);

            }
        }
    }

    private bool IsBuildingAllRow() {
        int acc = 0;
        foreach (SlotController slot in _slots) {
            if (slot.IsBuilding()) {
                acc++;
            }
        }
        
        return acc == _slots.Length;
    }

    private bool AreBuildingThisPiece(PieceController piece) {
        foreach (SlotController slot in _slots) {
            if (slot.IsBuilding(piece.GetPieceType())) {
                return true;
            }
            if (slot.IsBuilding()) {
                return false;
            }
        }
        return true;
    }
}
