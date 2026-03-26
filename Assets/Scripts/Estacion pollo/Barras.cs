using UnityEngine;
using UnityEngine.EventSystems;

public class Barras : MonoBehaviour, IDropHandler
{
    public Horno horno; // referencia

    public void OnDrop(PointerEventData eventData)
    {
        // buscamos directamente pollo
        Pollo pollo = eventData.pointerDrag.GetComponent<Pollo>();

        if (pollo == null) return;

        //  solo si el horno está listo
        if (!horno.hasBarras)
        {
            Debug.Log("Faltan barras en el horno");
            return;
        }

        //  solo si está crudo
        if (pollo.estado != Pollo.Estado.Crudo)
        {
            Debug.Log("Este pollo ya no está crudo");
            return;
        }

        // colocar en barras
        pollo.transform.SetParent(transform, false);
        pollo.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        // iniciar cocción
        pollo.StartCooking();

        Debug.Log("Pollo en cocción");
    }
}