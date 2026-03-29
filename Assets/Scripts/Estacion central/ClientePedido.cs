using UnityEngine;
using System.Collections.Generic;

public class ClientePedido : MonoBehaviour
{
    public Pedido pedido;
    public ClientePedidoUI ui;

    private void Awake()
    {
        pedido = null;
    }

    public void GenerarPedidoRandom()
    {
        pedido = new Pedido();

        pedido.plato = (TipoPlato)Random.Range(0, 2);
        pedido.bebida = (Bebida)Random.Range(0, 3);

        pedido.cremas = new List<Crema>();

        int cantidad = Random.Range(1, 3);

        // evitar duplicados
        List<Crema> disponibles = new List<Crema>
        {
            Crema.Mayo,
            Crema.Mostaza,
            Crema.Ketchup
        };

        for (int i = 0; i < cantidad && disponibles.Count > 0; i++)
        {
            int index = Random.Range(0, disponibles.Count);
            pedido.cremas.Add(disponibles[index]);
            disponibles.RemoveAt(index);
        }

        string cremasStr = "";

        foreach (var c in pedido.cremas)
        {
            cremasStr += c.ToString() + " ";
        }

        Debug.Log("Pedido: " + pedido.plato + " - " + pedido.bebida + " - [" + cremasStr + "]");
        if (ui != null)
            ui.RefreshUI();
    }

    public void LimpiarPedido()
    {
        pedido = null;
    }

    public bool TienePedido()
    {
        return pedido != null;
    }
}