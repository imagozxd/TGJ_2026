using UnityEngine;
using UnityEngine.EventSystems;

public class Caldero : MonoBehaviour, IDropHandler
{
    public bool isOn = false;

    public void OnDrop(PointerEventData eventData)
    {
        Fosforo fosforo = eventData.pointerDrag.GetComponent<Fosforo>();

        if (fosforo == null) return;

        if (isOn)
        {
            Debug.Log("El caldero ya está encendido");
            return;
        }

        isOn = true;

        Debug.Log("Caldero encendido");

        // opcional: consumir fósforo
        fosforo.gameObject.SetActive(false);
    }
}