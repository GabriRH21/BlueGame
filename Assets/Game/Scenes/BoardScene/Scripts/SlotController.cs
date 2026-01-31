using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotController : MonoBehaviour
{
    private PieceController _piece;
    private PieceType _pieceToBuild = PieceType.None;
    private Image _myImage;

    private void Awake() {
        _myImage = GetComponent<Image>();
    }

#region Getters
    public bool IsBuilding() {
        return _pieceToBuild != PieceType.None;
    }

    public bool IsBuilding(PieceType type) {
        return _pieceToBuild == type;
    }
#endregion Getters

#region Setters
    public void Build(PieceController piece) {
        _piece = piece;
        _pieceToBuild = piece.GetPieceType();
        _myImage.color = piece.GetColor32();
    }
#endregion Setters

}
