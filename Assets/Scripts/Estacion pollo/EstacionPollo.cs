using UnityEngine;
using System.Collections.Generic;

public class EstacionPollo : MonoBehaviour
{
    public Horno horno;
    public Barras barras;

    // referencia al mostrador
    public Mostrador mostrador;

    // prefab visual del pollo para el mostrador
    public GameObject polloPrefabMostrador;

    // Todos los objetos que deben resetearse (fosforo, barras, pollo, etc.)
    public List<GameObject> objetosReset = new List<GameObject>();

    // llamado cuando el pollo est� listo para enviarse
    public void EnviarPolloAlMostrador()
    {
        if (mostrador == null)
        {

            Debug.LogError("Mostrador no asignado");
            return;
        }

        if (polloPrefabMostrador == null)
        {
            Debug.LogError("Prefab de pollo no asignado");
            return;
        }

        mostrador.AgregarPlato(polloPrefabMostrador, TipoPlato.Pollo);

        Debug.Log("Pollo enviado al mostrador");
    }

    public void ResetEstacion()
    {
        Debug.Log("Reset Estacion Pollo");

        // Reset l�gico del horno
        if (horno != null)
            horno.ResetHorno();

        // Reset de pollos en barras
        if (barras != null)
            barras.ResetBarras();

        // Reset visual de todos los objetos
        foreach (var obj in objetosReset)
        {
            if (obj == null) continue;

            // reactivar objetos desactivados (ej: fosforo)
            obj.SetActive(true);

            InitialTransform init = obj.GetComponent<InitialTransform>();

            if (init != null)
            {
                RectTransform rect = obj.GetComponent<RectTransform>();

                // volver a su padre original
                obj.transform.SetParent(init.initialParent, false);

                // restaurar posici�n inicial
                rect.anchoredPosition = init.initialPosition;

                // asegurar orden visual
                obj.transform.SetAsLastSibling();
            }

            // reactivar drag
            var drag = obj.GetComponent<DraggableItem>();
            if (drag != null)
                drag.enabled = true;

            // reactivar interacci�n
            var cg = obj.GetComponent<CanvasGroup>();
            if (cg != null)
            {
                cg.blocksRaycasts = true;
                cg.interactable = true;
                cg.alpha = 1f;
            }

            Debug.Log("Reset objeto: " + obj.name);
        }
    }
}