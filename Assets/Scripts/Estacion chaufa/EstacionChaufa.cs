using UnityEngine;

public class EstacionChaufa : MonoBehaviour
{
    public Sarten sarten;
    public SliderCoccion slider;

    public Mostrador mostrador;
    public GameObject chaufaPrefabMostrador;

    // objetos a resetear (ingredientes, etc.)
    public GameObject[] objetosReset;

    // referencia al chaufa listo para ocultarlo en reset
    public GameObject chaufaListo;

    public void ResetEstacion()
    {
        Debug.Log("Reset Estacion Chaufa");

        if (sarten != null)
            sarten.ResetEstacion();

        if (slider != null)
            slider.ResetCoccion();

        // ocultar chaufa listo
        if (chaufaListo != null)
            chaufaListo.SetActive(false);

        // reset b�sico de objetos (posici�n + interacci�n)
        foreach (var obj in objetosReset)
        {
            if (obj == null) continue;

            obj.SetActive(true);

            var init = obj.GetComponent<InitialTransform>();
            if (init != null)
            {
                var rect = obj.GetComponent<RectTransform>();
                obj.transform.SetParent(init.initialParent, false);
                rect.anchoredPosition = init.initialPosition;
                obj.transform.SetAsLastSibling();
            }

            var cg = obj.GetComponent<CanvasGroup>();
            if (cg != null)
            {
                cg.blocksRaycasts = true;
                cg.interactable = true;
                cg.alpha = 1f;
            }

            var drag = obj.GetComponent<DraggableItem>();
            if (drag != null)
                drag.enabled = true;
        }
    }
}