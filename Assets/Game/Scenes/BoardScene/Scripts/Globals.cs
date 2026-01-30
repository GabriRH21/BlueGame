using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlayerData {
    public List<PieceController> PiecesInHand;

    public PlayerData(List<PieceController> ph) {
        PiecesInHand = ph;
    }
}

public static class Globals
{
    public static PlayerData PlayerStats = new PlayerData(new List<PieceController>());
}
