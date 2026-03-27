using UnityEngine;
using UnityEngine.EventSystems;

public class Flama : MonoBehaviour, IDropHandler
{
    public bool isOn = false;

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("DROP DETECTADO EN FLAMA");
        FosforoChaufa fosforo = eventData.pointerDrag.GetComponent<FosforoChaufa>();

        if (fosforo == null) return;

        isOn = true;
        fosforo.gameObject.SetActive(false);

        Debug.Log("Flama encendida");
    }
}