using UnityEngine;
using UnityEngine.EventSystems;

public class Horno : MonoBehaviour, IDropHandler
{
    public bool hasBarras = false;

    public void OnDrop(PointerEventData eventData)
    {
        // 1. Detectar barras
        BarrasItem barras = eventData.pointerDrag.GetComponent<BarrasItem>();

        if (barras != null && !hasBarras)
        {
            hasBarras = true;

            barras.transform.SetParent(transform, false);
            barras.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

            Debug.Log("Barras colocadas");
            return;
        }

        // 2. Detectar pollo
        Pollo pollo = eventData.pointerDrag.GetComponent<Pollo>();

        if (pollo != null)
        {
            if (!hasBarras)
            {
                Debug.Log("Faltan barras");
                return;
            }

            if (pollo.estado != Pollo.Estado.Crudo)
                return;

            pollo.transform.SetParent(transform, false);
            pollo.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

            pollo.StartCooking();

            Debug.Log("Pollo en horno");
        }
    }
}