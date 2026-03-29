using UnityEngine;
using System.Collections.Generic;

public class Mostrador : MonoBehaviour
{
    public Transform[] slots;

    private List<GameObject> platos = new List<GameObject>();

    // agregar plato desde estaciones
    public void AgregarPlato(GameObject platoPrefab, TipoPlato tipo)
    {
        if (platos.Count >= slots.Length)
        {
            Debug.Log("Mostrador lleno");
            return;
        }

        Transform slot = slots[platos.Count];

        GameObject go = Instantiate(platoPrefab, slot);
        go.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        PlatoData data = go.AddComponent<PlatoData>();
        data.tipo = tipo;

        // slot de pedido: gestiona bebida + cremas del plato
        SlotMostrador slotComp = go.AddComponent<SlotMostrador>();
        slotComp.platoData = data;

        // permitir click y drop de bebidas/cremas
        go.AddComponent<PlatoClickable>();

        platos.Add(go);

        Debug.Log("Plato agregado: " + tipo);
    }

    // eliminar plato seleccionado del mostrador
    public void RemoverPlato(PlatoData plato)
    {
        if (plato == null) return;

        if (platos.Contains(plato.gameObject))
        {
            // limpiar bebida/cremas del slot antes de destruir
            SlotMostrador slotComp = plato.GetComponent<SlotMostrador>();
            if (slotComp != null) slotComp.LimpiarVisuales();

            platos.Remove(plato.gameObject);
            Destroy(plato.gameObject);

            ReordenarPlatos();
        }
    }

    // reacomodar visualmente los platos
    void ReordenarPlatos()
    {
        for (int i = 0; i < platos.Count; i++)
        {
            platos[i].transform.SetParent(slots[i], false);
            platos[i].GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }
    }
}