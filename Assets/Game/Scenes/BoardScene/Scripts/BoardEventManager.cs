using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BoardEventManager
{
    public static System.Action<PieceController, BankController> PieceChoosenFromBank;
    public static System.Action<BankController> BankSelected;
}
