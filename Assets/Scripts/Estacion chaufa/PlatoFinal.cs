using UnityEngine;
using UnityEngine.EventSystems;

public class PlatoFinal : MonoBehaviour, IDropHandler
{
    public EstacionChaufa estacion; //  cambio

    public void OnDrop(PointerEventData eventData)
    {
        GameObject obj = eventData.pointerDrag;
        if (obj == null) return;

        Ingredient ingredient = obj.GetComponent<Ingredient>();
        if (ingredient == null || ingredient.type != IngredientType.Chaufa) return;

        obj.transform.SetParent(transform, false);
        obj.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        Debug.Log(" Chaufa servido");

        //  reset SOLO esta estaci�n
        estacion.ResetEstacion();
    }
}