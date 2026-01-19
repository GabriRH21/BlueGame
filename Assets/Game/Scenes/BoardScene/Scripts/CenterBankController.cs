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

        // 1️⃣ Buscar pieza del mismo tipo
        foreach (var piece in pieces) {
            if (piece.GetPieceType() == newPiece.GetPieceType()) {
                sameTypePiece = piece;
                break;
            }
        }

        // 2️⃣ Si existe, actualizar cantidad y destruir la nueva
        if (sameTypePiece != null) {
            int index = (int)sameTypePiece.GetPieceType();

            Debug.Log("cantidad: " + quantities[index]);
            if (quantities[index] == 0  || pieces.Count == 1) {
                sameTypePiece.TurnQuantityOn(true);
            }

            quantities[index]++;
            sameTypePiece.UpdateQuantity(quantities[index]);

            Destroy(newPiece.gameObject);
        }
        // 3️⃣ Si no existe, añadir la nueva
        else {
            pieces.Add(newPiece);
            quantities[(int)newPiece.GetPieceType()]++;
        }

        // 4️⃣ Activar centro si hace falta
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
