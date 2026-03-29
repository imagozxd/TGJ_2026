using UnityEngine;
using UnityEngine.EventSystems;

public class CremaItem : MonoBehaviour, IPointerClickHandler
{
    public Crema tipo;

    public void OnPointerClick(PointerEventData eventData)
    {
        SeleccionActual.Instance.ToggleCrema(tipo);
    }
}