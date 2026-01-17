using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class PieceController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public PieceType pieceType;
    private Color32 color;
    private void Awake()
    {
        SelectType();
        this.enabled = false;
    }

    private void SelectType()
    {
        PieceType[] values = (PieceType[]) System.Enum.GetValues(typeof(PieceType));
        pieceType = values[Random.Range(0, values.Length)];

        switch (pieceType)
        {
            case PieceType.Red:
                color = new Color32(255, 0, 0, 255);
                break;
            case PieceType.Green:
                color = new Color32(0, 236, 11, 255);
                break;
            case PieceType.Purple:
                color = new Color32(219, 0, 201, 255);
                break;
            case PieceType.Blue:
                color = new Color32(30, 144, 255, 255);
                break;
            default:
                color = new Color32(231, 242, 0, 255);
                break;
        }
        this.gameObject.GetComponent<Image>().color = color;
    }

    public void OnPointerEnter(PointerEventData p)
    {
        Debug.Log("Hola");
    }

    public void OnPointerExit(PointerEventData p)
    {
        
    }
}
