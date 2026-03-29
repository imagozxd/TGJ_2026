using UnityEngine;

public class SliderCoccion : MonoBehaviour
{
    public Sarten sarten;

    public int ciclosNecesarios = 3;
    private int ciclosActuales = 0;
    private bool fueACero = false;

    // objeto UI que se activará al terminar
    public GameObject chaufaListo;

    public void OnSliderChanged(float value)
    {
        if (!sarten.listoParaCocinar) return;

        if (value <= 0.1f && !fueACero)
        {
            fueACero = true;
        }

        if (value >= 0.9f && fueACero)
        {
            ciclosActuales++;
            fueACero = false;
        }

        if (ciclosActuales >= ciclosNecesarios)
        {
            sarten.listoParaCocinar = false;

            ActivarChaufaListo();
            OnCoccionCompleta();
        }
    }

    void ActivarChaufaListo()
    {
        if (chaufaListo == null)
        {
            Debug.LogError("chaufaListo no asignado");
            return;
        }

        chaufaListo.SetActive(true);

        // opcional: asegurarse que sea arrastrable
        var cg = chaufaListo.GetComponent<CanvasGroup>();
        if (cg != null)
        {
            cg.blocksRaycasts = true;
            cg.interactable = true;
            cg.alpha = 1f;
        }
    }

    void OnCoccionCompleta()
    {
        Debug.Log("Chaufa listo para entregar");
    }

    public void ResetCoccion()
    {
        ciclosActuales = 0;
        fueACero = false;
    }
}