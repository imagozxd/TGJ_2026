using UnityEngine;
using UnityEngine.EventSystems;

public class Plato : MonoBehaviour, IDropHandler
{
    public EstacionPollo estacion;

    public void OnDrop(PointerEventData eventData)
    {
        Pollo pollo = eventData.pointerDrag.GetComponent<Pollo>();

        if (pollo == null) return;

        if (!pollo.EsServible())
        {
            Debug.Log("No puedes servir pollo crudo");
            return;
        }

        pollo.StopCooking();

        pollo.transform.SetParent(transform, false);
        pollo.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        Debug.Log($" Plato listo con pollo: {pollo.estado}");

        //  RESET SOLO ESTA ESTACI�N
        estacion.ResetEstacion();
    }
}