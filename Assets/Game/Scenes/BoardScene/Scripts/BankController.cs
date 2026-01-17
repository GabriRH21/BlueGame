using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BankController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image _focus;

    private bool _hoverFlag = false;

    private void Awake()
    {
        this.gameObject.GetComponent<Button>().onClick.AddListener(SelectThisBank);
        _focus.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData p)
    {
        _hoverFlag = true;
        _focus.gameObject.SetActive(true);
        StartCoroutine(FocusAnimation());
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

        rect.localScale = endScale; // Asegura que llegue al final exacto
    }

    public void OnPointerExit(PointerEventData p)
    {
        _hoverFlag = false;
        StartCoroutine(ScaleTo(_focus.rectTransform, 1f, 0f));
        _focus.gameObject.SetActive(false);   
    }

    private void SelectThisBank()
    {
        
    }
}
