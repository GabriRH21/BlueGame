using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class PieceController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public PieceType pieceType;
    private Color32 color;
    private bool _hoverFlag = false;
    [SerializeField] private Image _focus;

    private void Awake()
    {
        SelectType();
        this.enabled = false;
        _focus.gameObject.SetActive(false);
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
        _hoverFlag = true;
        _focus.gameObject.SetActive(true);
        Debug.Log("hola");
        StartCoroutine(FocusAnimation());
    }

    public void OnPointerExit(PointerEventData p)
    {
        _hoverFlag = false;
        StartCoroutine(ScaleTo(_focus.rectTransform, 1f, 0f));
        _focus.gameObject.SetActive(false); 
    }

    private IEnumerator FocusAnimation()
    {
        while (_hoverFlag)
        {
            yield return StartCoroutine(ScaleTo(_focus.rectTransform, 1.25f, 0.5f));
            yield return StartCoroutine(ScaleTo(_focus.rectTransform, 1f, 0.5f));
            yield return new WaitForSeconds(0);
        }
    }

    private IEnumerator ScaleTo(RectTransform rect, float target, float duration)
    {
        Vector3 startScale = rect.localScale;
        Vector3 endScale = new Vector3(1,1,1) * target;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            rect.localScale = Vector3.Lerp(startScale, endScale, elapsed / duration);
            yield return null;
        }

        rect.localScale = endScale; 
    }
}
