using UnityEngine;
using UnityEngine.EventSystems;

public class PlatoFinalChaufa : MonoBehaviour, IDropHandler
{
    public EstacionChaufa estacion;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject obj = eventData.pointerDrag;

        if (obj == null) return;

        Chaufa chaufa = obj.GetComponent<Chaufa>();
        if (chaufa == null) return;

        Debug.Log("Drop Chaufa detectado");

        // enviar al mostrador
        if (estacion.mostrador != null)
        {
            estacion.mostrador.AgregarPlato(
                estacion.chaufaPrefabMostrador,
                TipoPlato.Chaufa
            );

            Debug.Log("Chaufa enviado al mostrador");
        }
        else
        {
            Debug.LogError("Mostrador no asignado");
        }

        // ocultar chaufa listo
        obj.SetActive(false);

        // reset de la estación
        estacion.ResetEstacion();
    }
}