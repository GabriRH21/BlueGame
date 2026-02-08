using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardSceneManager : MonoBehaviour
{
    [Header("CenterStuff")]
    [SerializeField] private GameObject _centerPiecePrefab;
    [SerializeField] private RectTransform[] _banks;
    [SerializeField] private RectTransform _center;
    [Space]
    [Header("PlayerBoardStuff")]
    [SerializeField] private RectTransform _playerBoard;
    [Space]
    [Header("UI")]
    [SerializeField] private UICanvasController _uiCanvas;
    [SerializeField] private Button _backZoomButton;
    [SerializeField] private UIZoomManager _uiZoomManager;

    private void Awake() {
        BoardEventManager.PieceChoosenFromBank += PieceChoosenFromBank;
        BoardEventManager.BankSelected += BankSelected;
        FillBanks();
        
    }

    private void FillBanks() {
        float radius = 25f;
        foreach (var bank in _banks) {
            for (int i = 0; i < 4; i++) {
                GameObject piece = Instantiate(_centerPiecePrefab, bank);

                RectTransform rt = piece.GetComponent<RectTransform>();

                float angle = (360f / 4) * i + Random.Range(-10f, 10f);
                Vector2 pos = new Vector2(
                    Mathf.Cos(angle * Mathf.Deg2Rad),
                    Mathf.Sin(angle * Mathf.Deg2Rad)
                ) * radius;

                rt.anchoredPosition = pos;
                rt.localRotation = Quaternion.Euler(0, 0, Random.Range(-25f, 25f));
            }
        }
    }
#region Events
    private void PieceChoosenFromBank(PieceController piece, BankController bank) {
        BankZoomOut(bank);
        
        if (bank is CenterBankController) {
            CenterBankController centerBank = bank as CenterBankController;
            _uiCanvas.ShowPieces(centerBank.GetQuantities(piece), piece);
            for (int i = 0; i < centerBank.GetQuantities(piece); i++) {
                Globals.PlayerStats.PiecesInHand.Add(piece);
            }

            
            if (centerBank.HasFirstPiece()) {
                BoardEventManager.AddCenterPenalty?.Invoke();
                centerBank.RemoveFirstPiece();
            }
            centerBank.RemovePiece(piece);
            centerBank.EnableCenter();
        } else {
            List<PieceController> allBrothers = piece.GetBrothers();
            allBrothers.Add(piece);
            
            if (!bank.Has(allBrothers)) {
                Debug.LogError("Las piezas Seleccionadas no pertenecen al banco seleccionado");
                return;
            }
            _uiCanvas.ShowPieces(allBrothers.Count, piece);
            // ToDo: Add Pieces To player, Allowing him to put whereever he wants
            foreach (var pieceToTake in allBrothers) {
                Globals.PlayerStats.PiecesInHand.Add(pieceToTake);
                bank.RemovePiece(pieceToTake);
            }

            CleanBank(bank);
        }
    }

    private void BankZoomOut(BankController bank){
        _uiZoomManager.ResetZoom(bank.gameObject.GetComponent<RectTransform>());
        _backZoomButton.gameObject.SetActive(false);
    }

    private void CleanBank(BankController bank) {
        List<PieceController> pieces = bank.GetPieces();
        float radius = 25f;

        BankController centerBank = _center.GetComponent<CenterBankController>();
        if (bank != centerBank) {
            foreach (var piece in pieces) {
                RectTransform rt = piece.GetComponent<RectTransform>();

                rt.SetParent(_center, worldPositionStays: false);

                float angle = (360f / 4) * centerBank.GetPieces().Count + Random.Range(-10f, 10f);
                Vector2 pos = new Vector2(
                    Mathf.Cos(angle * Mathf.Deg2Rad),
                    Mathf.Sin(angle * Mathf.Deg2Rad)
                ) * radius;

                rt.anchoredPosition = pos;
                rt.localRotation = Quaternion.Euler(0, 0, Random.Range(-25f, 25f));

                centerBank.AddPiece(piece);
            }

            bank.Clear();
        } else {
            //ToDo: remove the piece i took
        }

        
    }

    private void BankSelected(BankController actualBank) {
        if (Globals.PlayerStats.PiecesInHand.Count > 0) return;

        foreach(var bank in _banks) {

            if (bank.GetComponent<BankController>().IsBankSelected()) {
                return;
            }
        }

        actualBank.ConfirmSelection();
    }
#endregion
}
