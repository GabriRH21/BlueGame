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
        BoardEventManager.UpdatePiecesCounter += UpdatePiecesCounter;
        _piecesImage.gameObject.SetActive(false);
    }

    
    private void UpdatePiecesCounter(int quantity, PieceController piece = null) {
        if (quantity == 0 || piece == null) {
            HidePieces();
            return;
        }
        ShowPieces(quantity, piece);
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
