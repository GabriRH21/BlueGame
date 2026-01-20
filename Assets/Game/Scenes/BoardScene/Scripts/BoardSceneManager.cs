using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSceneManager : MonoBehaviour
{
    [Header("CenterStuff")]
    [SerializeField] private GameObject _centerPiecePrefab;
    [SerializeField] private RectTransform[] _banks;
    [SerializeField] private RectTransform _center;
    [Space]
    [Header("PlayerBoardStuff")]
    [SerializeField] private RectTransform _playerBoard;

    private void Awake() {
        BoardEventManager.PieceChoosenFromBank += PieceChoosenFromBank;
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

    private void PieceChoosenFromBank(PieceController piece, BankController bank) {
        List<PieceController> allBrothers = piece.GetBrothers();
        allBrothers.Add(piece);
        if (!bank.Has(allBrothers)) {
            Debug.LogError("Las piezas Seleccionadas no pertenecen al banco seleccionado");
            return;
        }
        // ToDo: Add Pieces To player, Allowing him to put whereever he wants
        foreach (var pieceToTake in allBrothers) {
            bank.RemovePiece(pieceToTake);
        }

        CleanBank(bank);
    }

    private void CleanBank(BankController bank) {
        List<PieceController> pieces = bank.GetPieces();
        float radius = 25f;

        BankController centerBank = _center.GetComponent<CenterBankController>();

        foreach (var piece in pieces)
        {
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
    }
}
