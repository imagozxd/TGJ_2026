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
            Debug.Log("El caldero ya est� encendido");
            return;
        }

        isOn = true;

        Debug.Log("Caldero encendido");

        fosforo.CleanupGhost();
        fosforo.fueEntregado = true;
        fosforo.gameObject.SetActive(false);
    }
}