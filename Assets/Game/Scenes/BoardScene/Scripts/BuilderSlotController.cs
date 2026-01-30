using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuilderSlotController : MonoBehaviour
{
    private PieceController _piece;
    [SerializeField] private Button _button;
    [SerializeField] private BuilderSlotController[] _brothers;

    private void Awake() {
        _button.onClick.AddListener(SlotSelected);
    }

    private void SlotSelected() {
        
    }
}
