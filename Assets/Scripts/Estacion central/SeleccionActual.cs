using System.Collections.Generic;
using UnityEngine;

public class SeleccionActual : MonoBehaviour
{
    public static SeleccionActual Instance;

    public PlatoData platoSeleccionado;
    public Bebida bebidaSeleccionada;
    public bool tieneBebidaSeleccionada;
    public List<Crema> cremasSeleccionadas = new List<Crema>();

    private void Awake()
    {
        Instance = this;
    }

    public void SeleccionarPlato(PlatoData plato)
    {
        platoSeleccionado = plato;
        Debug.Log("Plato seleccionado: " + plato.tipo);
    }

    public void SeleccionarBebida(Bebida bebida)
    {
        bebidaSeleccionada = bebida;
        tieneBebidaSeleccionada = true;
        Debug.Log("Bebida seleccionada: " + bebida);
    }

    public void ToggleCrema(Crema crema)
    {
        if (cremasSeleccionadas.Contains(crema))
        {
            cremasSeleccionadas.Remove(crema);
            Debug.Log("Crema removida: " + crema);
        }
        else
        {
            cremasSeleccionadas.Add(crema);
            Debug.Log("Crema agregada: " + crema);
        }
    }

    public void Limpiar()
    {
        platoSeleccionado = null;
        bebidaSeleccionada = default;
        tieneBebidaSeleccionada = false;
        cremasSeleccionadas.Clear();
    }
}