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
        
        PlacePieces(piece, numberOfPieces);
    }

    private int FreeSlots() {
        int acc = 0;
        foreach (SlotController slot in _slots) {
            if (!slot.IsBuilding()) {
                acc++;
            }
        }
        return acc;
    }

    private void PlacePieces(PieceController piece, int numberOfPieces) {
        int places = System.Math.Min(numberOfPieces, FreeSlots());
        int index = 0;
        try {
            do {
                if (!_slots[index].IsBuilding()) {
                    _slots[index].Build(piece);
                    places--;
                }
                index++;
            } while(places != 0 && index < _slots.Length);
        } catch (System.Exception e) {
            Debug.LogError("Error: FreeSlots is not calculating well. Error: " + e.Message);
        }

        Globals.PlayerStats.PiecesInHand = new List<PieceController>();
        BoardEventManager.UpdatePiecesCounter?.Invoke(0, null);
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
