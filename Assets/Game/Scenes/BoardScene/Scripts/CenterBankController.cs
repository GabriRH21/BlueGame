using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterBankController : BankController
{
    private int[] quantities= {0, 0, 0, 0, 0};
    private bool centerIsActive = false;
    protected override void Awake() {
        base.Awake();
        _button.enabled = false;
    }

    public override void AddPiece(PieceController newPiece) {
        if (pieces.Contains(newPiece)) {
            Debug.LogWarning("repeated piece in Center");
            return;
        }

        PieceController sameTypePiece = null;

        foreach (var piece in pieces) {
            if (piece.GetPieceType() == newPiece.GetPieceType()) {
                sameTypePiece = piece;
                break;
            }
        }

        if (sameTypePiece != null) {
            int index = (int)sameTypePiece.GetPieceType();

            if (quantities[index] == 0  || pieces.Count == 1) {
                sameTypePiece.TurnQuantityOn(true);
            }

            quantities[index]++;
            sameTypePiece.UpdateQuantity(quantities[index]);

            Destroy(newPiece.gameObject);
        } else {
            pieces.Add(newPiece);
            quantities[(int)newPiece.GetPieceType()]++;
        }

        if (!centerIsActive) {
            EnableCenter();
            centerIsActive = true;
        }
    }

    private void EnableCenter() {
        _button.enabled = true;
        _focus.enabled = true;
    }
}
