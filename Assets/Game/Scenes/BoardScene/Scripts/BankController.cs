using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BankController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image _focus;
    [SerializeField] private Button _button;
    [SerializeField] private UIZoomManager _zoomManager;
    [SerializeField] private Button _backButton;

    private List<PieceController> pieces = new List<PieceController>();

    private bool _hoverFlag = false;
    private bool _bankSelected = false;

    private void Awake()
    {
        _button.onClick.AddListener(SelectThisBank);
        _focus.gameObject.SetActive(false);
        _backButton.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData p)
    {
        if (!_bankSelected) {
           _hoverFlag = true;
            _focus.gameObject.SetActive(true);
            StartCoroutine(FocusAnimation()); 
        }
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

    public void OnPointerExit(PointerEventData p)
    {
        _hoverFlag = false;
        StartCoroutine(ScaleTo(_focus.rectTransform, 1f, 0f));
        _focus.gameObject.SetActive(false);   
    }

    private void SelectThisBank()
    {
        _button.enabled = false;
        _bankSelected = true;
        
        _hoverFlag = false;
        StartCoroutine(ScaleTo(_focus.rectTransform, 1f, 0f));
        _focus.gameObject.SetActive(false);
        _zoomManager.ZoomTo(GetComponent<RectTransform>());
        _backButton.gameObject.SetActive(true);                             //ToDo: maybe I can try to do a fadein animation for the button
        _backButton.onClick.RemoveAllListeners();
        _backButton.onClick.AddListener(OnBackButtonPressed);
        ActivatePieces(true);
    }

    private void OnBackButtonPressed()
    {
        _bankSelected = false;
        _zoomManager.ResetZoom(GetComponent<RectTransform>());
        _button.enabled = true;
        _backButton.gameObject.SetActive(false);
        _focus.gameObject.SetActive(true);
        ActivatePieces(false);
    }

    private void ActivatePieces(bool activate)
    {
        for (int i = 0; i < transform.childCount; i++) {
            Transform child = transform.GetChild(i);
            if (child.gameObject.GetComponent<PieceController>() != null) {
                PieceController piece = child.gameObject.GetComponent<PieceController>();
                piece.enabled = activate;
                
                if (activate) {
                    pieces.Add(piece);
                    piece.TurnOn();
                    SetBrothers(piece);
                } else {
                    piece.turnOff();
                }
            }
        }
        
        if (!activate) {
            pieces = new List<PieceController>();
        }
    }

    private void SetBrothers(PieceController newPiece)
    {
        foreach (var piece in pieces) {
            if (piece.pieceType == newPiece.pieceType && piece != newPiece) {
                piece.AddBrother(newPiece);
                newPiece.AddBrother(piece);
            }
        }
    }
}
