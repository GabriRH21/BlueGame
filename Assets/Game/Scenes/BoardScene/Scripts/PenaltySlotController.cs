using System.Collections;
using System.Collections.Generic; 
using UnityEngine;
using UnityEngine.UI;

public class PenaltySlotController : MonoBehaviour
{
    [SerializeField] private Image _myImage;
    [SerializeField] private Sprite _defaultPenalty;
    [SerializeField] private int _penalty;
    private bool _hasPenalty = false;

    public bool HasPenalty() {
        return _hasPenalty;
    }

    public void ConfirmPenalty() {
        _hasPenalty = true; 
        _myImage.sprite = _defaultPenalty;
    }

    public void ConfirmPenalty(PieceController piece) {
        _hasPenalty = true; 
        _myImage.color = piece.GetColor32();
        //ToDo: Clean penalties at the end of the turn
    }
}
