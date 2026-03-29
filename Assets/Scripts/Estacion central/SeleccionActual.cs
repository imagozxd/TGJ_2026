using System.Collections.Generic;
using UnityEngine;

public class SeleccionActual : MonoBehaviour
{
    public static SeleccionActual Instance;

    // slot seleccionado (plato + bebida + cremas)
    public SlotMostrador slotSeleccionado;

    // campos copiados del slot (usados por EntregaSimple)
    public PlatoData platoSeleccionado;
    public Bebida bebidaSeleccionada;
    public TamañoBebida tamañoBebidaSeleccionada;
    public bool tieneBebidaSeleccionada;
    public List<Crema> cremasSeleccionadas = new List<Crema>();

    private void Awake()
    {
        Instance = this;
    }

    // ── Selección por slot ────────────────────────────────────────
    public void SeleccionarSlot(SlotMostrador slot)
    {
        slotSeleccionado = slot;

        // copiar datos del slot a los campos que usa EntregaSimple
        platoSeleccionado         = slot.platoData;
        tieneBebidaSeleccionada   = slot.tieneBebida;
        bebidaSeleccionada        = slot.bebida;
        tamañoBebidaSeleccionada  = slot.tamañoBebida;
        cremasSeleccionadas       = new List<Crema>(slot.cremas);

        Debug.Log("Slot seleccionado - Plato: " + slot.platoData?.tipo
            + " | Bebida: " + (slot.tieneBebida ? slot.bebida + " " + slot.tamañoBebida : "ninguna")
            + " | Cremas: " + slot.cremas.Count);
    }

    // ── Selección directa de plato (sin slot, para compatibilidad) ─
    public void SeleccionarPlato(PlatoData plato)
    {
        platoSeleccionado = plato;
        Debug.Log("Plato seleccionado: " + plato.tipo);
    }

    // ── Limpiar selección (no destruye visuales del slot) ─────────
    public void Limpiar()
    {
        slotSeleccionado          = null;
        platoSeleccionado         = null;
        bebidaSeleccionada        = default;
        tamañoBebidaSeleccionada  = default;
        tieneBebidaSeleccionada   = false;
        cremasSeleccionadas.Clear();
    }
}