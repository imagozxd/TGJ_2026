using UnityEngine;
using UnityEngine.EventSystems;

public class PlatoClickable : MonoBehaviour, IPointerClickHandler
{
    private PlatoData data;

    private void Awake()
    {
        data = GetComponent<PlatoData>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SeleccionActual.Instance.SeleccionarPlato(data);
    }
}