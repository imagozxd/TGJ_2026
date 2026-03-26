using UnityEngine;
using System.Collections.Generic;

public class EstacionPollo : MonoBehaviour
{
    public Horno horno;
    public Barras barras;

    // Todos los objetos que deben resetearse (fosforo, barras, pollo, etc.)
    public List<GameObject> objetosReset = new List<GameObject>();

    public void ResetEstacion()
    {
        Debug.Log("Reset Estacion Pollo");

        // Reset lógico del horno
        if (horno != null)
            horno.ResetHorno();

        // Reset de pollos
        if (barras != null)
            barras.ResetBarras();

        // Reset visual de todos los objetos
        foreach (var obj in objetosReset)
        {
            if (obj == null) continue;

            // Clave para objetos desactivados (como fosforo)
            obj.SetActive(true);

            InitialTransform init = obj.GetComponent<InitialTransform>();

            if (init != null)
            {
                RectTransform rect = obj.GetComponent<RectTransform>();

                obj.transform.SetParent(init.initialParent, false);
                rect.anchoredPosition = init.initialPosition;
                obj.transform.SetAsLastSibling();
            }

            // Reactivar drag
            var drag = obj.GetComponent<UIDragDrop>();
            if (drag != null)
                drag.enabled = true;

            // Reactivar interacción
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