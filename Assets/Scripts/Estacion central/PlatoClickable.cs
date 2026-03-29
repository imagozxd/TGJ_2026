using UnityEngine;
using UnityEngine.EventSystems;

// Recibe clics para seleccionar el slot completo
// y recibe drops de BebidaItem/CremaItem para añadirlos al slot.
public class PlatoClickable : MonoBehaviour, IPointerClickHandler, IDropHandler
{
    private SlotMostrador slot;

    private void Awake()
    {
        slot = GetComponent<SlotMostrador>();
    }

    // ── Click: seleccionar el slot ────────────────────────────────
    public void OnPointerClick(PointerEventData eventData)
    {
        if (slot != null)
            SeleccionActual.Instance.SeleccionarSlot(slot);
    }

    // ── Drop: bebida o crema al slot ──────────────────────────────
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null) return;
        if (slot == null) return;

        // intentar como bebida
        BebidaItem bebida = eventData.pointerDrag.GetComponent<BebidaItem>();
        if (bebida != null)
        {
            slot.AgregarBebida(bebida);
            bebida.CleanupGhost();
            bebida.fueEntregado = true;
            return;
        }

        // intentar como crema
        CremaItem crema = eventData.pointerDrag.GetComponent<CremaItem>();
        if (crema != null)
        {
            slot.AgregarCrema(crema);
            crema.CleanupGhost();
            crema.fueEntregado = true;
        }
    }
}
