using UnityEngine;
using UnityEngine.EventSystems;

public class PlatoFinal : MonoBehaviour, IDropHandler
{
    public EstacionChaufa estacion; //  cambio

    public void OnDrop(PointerEventData eventData)
    {
        Chaufa chaufa = eventData.pointerDrag.GetComponent<Chaufa>();

        if (chaufa == null) return;

        chaufa.transform.SetParent(transform, false);
        chaufa.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        Debug.Log(" Chaufa servido");

        //  reset SOLO esta estación
        estacion.ResetEstacion();
    }
}