using UnityEngine;
using System.Collections.Generic;

public class ClientePedido : MonoBehaviour
{
    public Pedido pedido;
    public Cliente cliente;
    //public ClientePedidoUI ui;

    private void Awake()
    {
        pedido = null;
    }

    public void GenerarPedidoRandom()
    {
        pedido = new Pedido();

        pedido.plato = (TipoPlato)Random.Range(0, 2);

        pedido.pideBebida = Random.Range(0, 2) == 1;
        if (pedido.pideBebida)
        {
            pedido.bebida = (Bebida)Random.Range(0, 3);
            pedido.tamañoBebida = (TamañoBebida)Random.Range(0, 3);
        }
        else
        {
            pedido.bebida = default;
            pedido.tamañoBebida = default;
        }

        int cantidad = Random.Range(0, 3);
        if (cantidad == 0)
        {
            pedido.cremas = null;
        }
        else
        {
            pedido.cremas = new List<Crema>();

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
        }

        string bebidaStr = pedido.pideBebida ? pedido.bebida.ToString() : "(sin bebida)";
        string cremasStr = "(sin cremas)";
        if (pedido.cremas != null && pedido.cremas.Count > 0)
        {
            cremasStr = "";
            foreach (var c in pedido.cremas)
                cremasStr += c + " ";
        }

        Debug.Log("Pedido: " + pedido.plato + " - " + bebidaStr + " - [" + cremasStr + "]");
/*        if (ui != null)
            ui.RefreshUI();*/
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