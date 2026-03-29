using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(CanvasGroup))]
public class BebidaDraggable : MonoBehaviour,
    IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Bebida tipo;
    [SerializeField] private TamañoBebida tamaño;
    [SerializeField] private Sprite spriteAsignado; // sprite que muestra el placeholder estático

    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    // BebidaDropZone lo marca true antes de que OnEndDrag destruya el objeto
    [HideInInspector] public bool fueEntregado = false;

    public Bebida Tipo   => tipo;
    public TamañoBebida Tamaño => tamaño;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas        = GetComponentInParent<Canvas>();
        canvasGroup   = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // crear placeholder en la misma posicion, mismo padre, mismo orden visual
        GameObject placeholder = Instantiate(gameObject, transform.parent);
        placeholder.transform.SetSiblingIndex(transform.GetSiblingIndex());

        RectTransform phRect = placeholder.GetComponent<RectTransform>();
        phRect.anchoredPosition = rectTransform.anchoredPosition;

        // asignar sprite al placeholder
        Image phImage = placeholder.GetComponent<Image>();
        if (phImage != null && spriteAsignado != null)
            phImage.sprite = spriteAsignado;

        // el placeholder no debe ser draggable
        BebidaDraggable phDrag = placeholder.GetComponent<BebidaDraggable>();
        if (phDrag != null)
            Destroy(phDrag);

        // el original se arrastra: deshabilitar raycasts para que el drop zone lo detecte
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.6f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;

        // si no lo recibió ningún drop zone válido, destruir
        if (!fueEntregado)
            Destroy(gameObject);
    }
}
