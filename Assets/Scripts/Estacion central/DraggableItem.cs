using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(CanvasGroup))]
public abstract class DraggableItem : MonoBehaviour,
    IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector] public bool fueEntregado = false;

    protected RectTransform rectTransform;
    protected Canvas canvas;
    protected CanvasGroup canvasGroup;
    private Vector2 dragOffset;
    private GameObject ghost;
    private Vector2 originalPosition;

    protected virtual void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas        = GetComponentInParent<Canvas>();
        canvasGroup   = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        fueEntregado = false;
        originalPosition = rectTransform.anchoredPosition;

        // crear ghost visual — este sigue al cursor; el original nunca se mueve
        ghost = Instantiate(gameObject, transform.parent);
        ghost.transform.SetSiblingIndex(transform.GetSiblingIndex() + 1);
        RectTransform ghostRect = ghost.GetComponent<RectTransform>();
        ghostRect.anchoredPosition = rectTransform.anchoredPosition;

        // el ghost no debe responder a eventos de drag
        foreach (var d in ghost.GetComponents<DraggableItem>())
            Destroy(d);

        // el ghost no bloquea raycasts al drop zone
        CanvasGroup ghostCG = ghost.GetComponent<CanvasGroup>();
        if (ghostCG == null) ghostCG = ghost.AddComponent<CanvasGroup>();
        ghostCG.blocksRaycasts = false;

        ConfigurarGhost(ghost);

        // el original no bloquea raycasts mientras se arrastra
        canvasGroup.blocksRaycasts = false;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 localCursor
        );
        dragOffset = ghostRect.anchoredPosition - localCursor;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (ghost == null) return;

        // mantener el original fijo siempre
        rectTransform.anchoredPosition = originalPosition;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 localCursor
        );
        ghost.GetComponent<RectTransform>().anchoredPosition = localCursor + dragOffset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        CleanupGhost();
    }

    // llamar desde scripts externos antes de deshabilitar el componente
    public void CleanupGhost()
    {
        if (ghost != null)
        {
            Destroy(ghost);
            ghost = null;
        }
    }

    // subclase aplica sprite, escala y alpha al ghost
    protected abstract void ConfigurarGhost(GameObject ghost);
}
