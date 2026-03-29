using UnityEngine;
using UnityEngine.EventSystems;

public class PlatoPolloFinal : MonoBehaviour, IDropHandler
{
    public EstacionPollo estacion;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject obj = eventData.pointerDrag;

        if (obj == null) return;

        Pollo pollo = obj.GetComponent<Pollo>();
        if (pollo == null) return;

        // validar estado (no crudo)
        if (pollo.estado == Pollo.Estado.Crudo)
        {
            Debug.Log("No puedes servir pollo crudo");
            return;
        }

        Debug.Log("Pollo servido");

        // enviar al mostrador
        if (estacion.mostrador != null)
        {
            estacion.mostrador.AgregarPlato(
                estacion.polloPrefabMostrador,
                TipoPlato.Pollo
            );

            Debug.Log("Pollo enviado al mostrador");
        }
        else
        {
            Debug.LogError("Mostrador no asignado");
        }

        // detener cocción
        pollo.StopCooking();

        // ocultar objeto (se consume)
        obj.SetActive(false);

        // reset estación
        estacion.ResetEstacion();
    }
}