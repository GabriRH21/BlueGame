using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class PieceController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] private Button _button;
    [SerializeField] private Image _focus;
    [SerializeField] private TextMeshProUGUI _quantity;
    public PieceType pieceType;
    private Color32 color;
    private bool _hoverFlag = false;
    private List<PieceController> Brothers = new List<PieceController>();
    private BankController _bank;

    private void Awake() {
        SelectType();


        _focus.gameObject.SetActive(false);
        _button.onClick.AddListener(OnButtonClick);
        _button.enabled = false;
        _quantity.gameObject.SetActive(false);

        this.enabled = false;
    }

    public void TurnOn() {
        _button.enabled = true;

    }

    public void turnOff() {
        _button.enabled = false;
        this.enabled = false;
    }

    public void TurnQuantityOn(bool turnOn) {
        _quantity.gameObject.SetActive(turnOn);
    }

    public void UpdateQuantity(int newVal) {
        _quantity.text = newVal.ToString();
    }

    public PieceType GetPieceType() {
        return pieceType;
    }

    private void OnButtonClick() {
        BoardEventManager.PieceChoosenFromBank?.Invoke(this, _bank);
    }

    private void SelectType() {
        PieceType[] values = (PieceType[]) System.Enum.GetValues(typeof(PieceType));
        pieceType = values[Random.Range(0, values.Length)];

        switch (pieceType) {
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

    public void OnPointerEnter(PointerEventData p) {
        FocusAnim(true);
        foreach (var bro in Brothers) {
            bro.FocusAnim(true);
        }
    }

    public void OnPointerExit(PointerEventData p)
    {
        FocusAnim(false);
        foreach (var bro in Brothers) {
            bro.FocusAnim(false);
        }
    }

    public void FocusAnim(bool active) {
        _hoverFlag = active;
        _focus.gameObject.SetActive(active);
        if (active) {
            StartCoroutine(FocusAnimation());
        } else {
            StartCoroutine(ScaleTo(_focus.rectTransform, 1f, 0f));
        }
        
    }

    public void AddBrother(PieceController brother) {
        if (!Brothers.Contains(brother)) {
            Brothers.Add(brother);
        }
    }

    public void AddBank(BankController father) {
        _bank = father;
    }

    public List<PieceController> GetBrothers() {
        return Brothers;
    }

    private IEnumerator FocusAnimation() {
        while (_hoverFlag) {
            yield return StartCoroutine(ScaleTo(_focus.rectTransform, 1.25f, 0.5f));
            yield return StartCoroutine(ScaleTo(_focus.rectTransform, 1f, 0.5f));
            yield return new WaitForSeconds(0);
        }
    }

    private IEnumerator ScaleTo(RectTransform rect, float target, float duration) {
        Vector3 startScale = rect.localScale;
        Vector3 endScale = new Vector3(1,1,1) * target;
        float elapsed = 0f;

        while (elapsed < duration) {
            elapsed += Time.deltaTime;
            rect.localScale = Vector3.Lerp(startScale, endScale, elapsed / duration);
            yield return null;
        }

        rect.localScale = endScale; 
    }
}
