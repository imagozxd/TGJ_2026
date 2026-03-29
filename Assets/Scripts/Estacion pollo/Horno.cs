using UnityEngine;
using UnityEngine.EventSystems;

public class Horno : MonoBehaviour, IDropHandler
{
    public bool hasBarras = false;

    public Barras barras; //  referencia al sistema de barras

    public void OnDrop(PointerEventData eventData)
    {
        // 1. Detectar barras
        Barras barrasItem = eventData.pointerDrag.GetComponent<Barras>();

        if (barrasItem != null && !hasBarras)
        {
            hasBarras = true;

            DraggableItem dBarras = barrasItem.GetComponent<DraggableItem>();
            if (dBarras != null) { dBarras.CleanupGhost(); dBarras.fueEntregado = true; }

            barrasItem.transform.SetParent(transform, false);
            barrasItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

            Debug.Log(" Barras colocadas");
            return;
        }

        // 2. Detectar pollo
        Pollo pollo = eventData.pointerDrag.GetComponent<Pollo>();

        if (pollo != null)
        {
            if (!hasBarras)
            {
                Debug.Log(" Faltan barras");
                return;
            }

            if (pollo.estado != Pollo.Estado.Crudo)
                return;

            DraggableItem dPollo = pollo.GetComponent<DraggableItem>();
            if (dPollo != null) { dPollo.CleanupGhost(); dPollo.fueEntregado = true; }

            pollo.transform.SetParent(transform, false);
            pollo.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

            pollo.StartCooking();

            //  REGISTRAR POLLO (CLAVE PARA RESET)
            if (barras != null)
            {
                barras.RegistrarPollo(pollo.gameObject);
                Debug.Log(" Pollo registrado en barras");
            }

            Debug.Log(" Pollo en horno");
        }
    }

    //  Reset l�gico del horno
    public void ResetHorno()
    {
        hasBarras = false;
        Debug.Log(" Horno reseteado");
    }
}