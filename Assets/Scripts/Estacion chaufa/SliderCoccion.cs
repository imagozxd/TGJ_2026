using UnityEngine;

public class SliderCoccion : MonoBehaviour
{
    public Sarten sarten;

    public int ciclosNecesarios = 3;
    private int ciclosActuales = 0;

    private bool fueACero = false;

    public GameObject chaufaPreparado;

    public void OnSliderChanged(float value)
    {
        Debug.Log("Slider: " + value);

        if (!sarten.listoParaCocinar) return;

        // detectar que llegó a 0
        if (value <= 0.1f && !fueACero)
        {
            fueACero = true;
            Debug.Log(" Llegó a 0");
        }

        // detectar que llegó a 1 después
        if (value >= 0.9f && fueACero)
        {
            ciclosActuales++;
            fueACero = false;

            Debug.Log($" Ciclo: {ciclosActuales}/{ciclosNecesarios}");
        }

        if (ciclosActuales >= ciclosNecesarios)
        {
            Debug.Log(" CHAUFA LISTO");

            sarten.listoParaCocinar = false;

            ActivarChaufa();   
            OnCoccionCompleta();
        }
    }

    void ActivarChaufa()
    {
        if (chaufaPreparado == null)
        {
            Debug.LogError(" Chaufa no asignado");
            return;
        }

        chaufaPreparado.SetActive(true);

        Debug.Log(" Chaufa activado");
    }

    void OnCoccionCompleta()
    {
        Debug.Log(" POPUP AQUÍ");
    }

    public bool EstaListo()
    {
        return ciclosActuales >= ciclosNecesarios;
    }
    public void ResetCoccion()
    {
        Debug.Log(" Reset Slider");

        ciclosActuales = 0;
        fueACero = false;
    }
}