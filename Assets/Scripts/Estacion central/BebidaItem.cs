using UnityEngine;
using UnityEngine.EventSystems;

public class BebidaItem : MonoBehaviour, IPointerClickHandler
{
    public Bebida tipo;

    public void OnPointerClick(PointerEventData eventData)
    {
        SeleccionActual.Instance.SeleccionarBebida(tipo);
    }
}