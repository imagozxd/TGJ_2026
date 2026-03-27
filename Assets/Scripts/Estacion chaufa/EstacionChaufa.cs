using UnityEngine;

public class EstacionChaufa : MonoBehaviour
{
    public Sarten sarten;
    public SliderCoccion slider;
    public GameObject chaufaPreparado;
    

    public void ResetEstacion()
    {
        Debug.Log(" Reset Estación Chaufa");

        // reset ingredientes (sartén)
        if (sarten != null)
            sarten.ResetEstacion();

        // reset slider
        if (slider != null)
        {
            slider.ResetCoccion();

            var uiSlider = slider.GetComponent<UnityEngine.UI.Slider>();
            if (uiSlider != null)
                uiSlider.value = 0.5f;
        }

        // ocultar chaufa preparado
        if (chaufaPreparado != null)
            chaufaPreparado.SetActive(false);
    }
}