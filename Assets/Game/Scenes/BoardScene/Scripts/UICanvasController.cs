using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICanvasController : MonoBehaviour
{
    [Header("Pieces Counter")]
    [SerializeField] private RectTransform _piecesCounterContainer;
    [SerializeField] private Image _piecesImage;
    [SerializeField] private TextMeshProUGUI _piecesQuantity;
    // ToDo: [SerializeField] private Sprite[] _pieceSprite

    private void Awake() {
        _piecesImage.gameObject.SetActive(false);
    }

    public void ShowPieces(int number, PieceController piece) {
        _piecesImage.gameObject.SetActive(true);
        _piecesQuantity.text = number.ToString();
        //ToDo: _piecesImage.sprite = _piecesImage[(int)piece.GetPieceType()];
        // ToRemove:
        _piecesImage.color = piece.GetColor32();
    }

    public void HidePieces() {
        _piecesImage.gameObject.SetActive(false);
        _piecesQuantity.text = "x0";
    }


}
