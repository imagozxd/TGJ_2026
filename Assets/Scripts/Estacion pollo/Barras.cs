using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Barras : MonoBehaviour
{
    private List<GameObject> pollos = new List<GameObject>();

    public void RegistrarPollo(GameObject pollo)
    {
        if (!pollos.Contains(pollo))
        {
            pollos.Add(pollo);
            Debug.Log(" Pollo agregado a lista");
        }
    }

    public void ResetBarras()
    {
        Debug.Log(" Reset Barras");

        Debug.Log("Pollos registrados: " + pollos.Count);

        foreach (var pollo in pollos)
        {
            if (pollo == null) continue;

            InitialTransform init = pollo.GetComponent<InitialTransform>();

            if (init != null)
            {
                RectTransform rect = pollo.GetComponent<RectTransform>();

                //  volver al parent original
                pollo.transform.SetParent(init.initialParent, false);

                //  restaurar posición
                rect.anchoredPosition = init.initialPosition;

                //  asegurar que se vea encima
                pollo.transform.SetAsLastSibling();

                //  forzar UI update
                LayoutRebuilder.ForceRebuildLayoutImmediate(rect);
            }

            // 🔄 reset estado del pollo
            Pollo p = pollo.GetComponent<Pollo>();
            if (p != null)
            {
                p.StopCooking();
                p.ResetPollo();
            }

            // 🔓 reactivar drag
            var drag = pollo.GetComponent<UIDragDrop>();
            if (drag != null)
                drag.enabled = true;

            // 🔓 reactivar interacción
            var cg = pollo.GetComponent<CanvasGroup>();
            if (cg != null)
            {
                cg.blocksRaycasts = true;
                cg.interactable = true;
                cg.alpha = 1f;
            }

            Debug.Log(" Pollo reseteado: " + pollo.name);
        }

        pollos.Clear();
    }
}