using UnityEngine;
using UnityEngine.EventSystems;

public class BebidaItem : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Bebida tipo;
    [SerializeField] private TamañoBebida tamaño;

    public void OnPointerClick(PointerEventData eventData)
    {
        SeleccionActual.Instance.SeleccionarBebida(tipo, tamaño);
    }
}