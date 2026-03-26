using UnityEngine;
using UnityEngine.EventSystems;

public class Plato : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        Pollo pollo = eventData.pointerDrag.GetComponent<Pollo>();

        if (pollo == null) return;

        //  opcional: bloquear crudo
        if (pollo.estado == Pollo.Estado.Crudo)
        {
            Debug.Log("No puedes servir pollo crudo");
            return;
        }

        // detener cocción siempre
        pollo.StopCooking();

        // colocar en plato
        pollo.transform.SetParent(transform, false);
        pollo.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        Debug.Log($"Plato listo con pollo: {pollo.estado}");
    }
}