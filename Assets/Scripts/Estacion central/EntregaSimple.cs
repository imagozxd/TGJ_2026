using UnityEngine;
using System.Collections;

public class EntregaSimple : MonoBehaviour
{
    public ClientePedido cliente;
    public Mostrador mostrador;

    public float tiempoPenalizacion = 2f;

    public void Entregar()
    {
        var sel = SeleccionActual.Instance;

        // validaciones b�sicas
        if (cliente == null || cliente.pedido == null)
        {
            Debug.Log("No hay cliente o pedido");
            return;
        }

        if (sel.platoSeleccionado == null)
        {
            Debug.Log("No seleccionaste plato");
            return;
        }

        if (mostrador == null)
        {
            Debug.LogError("Mostrador no asignado");
            return;
        }

        bool correcto = ValidarEntrega(sel);

        if (correcto)
        {
            Debug.Log("Entrega correcta");

            // remover plato del mostrador
            mostrador.RemoverPlato(sel.platoSeleccionado);

            // limpiar cliente (no destruir slot)
            cliente.pedido = null;

            // opcional: regenerar otro pedido o dejar vac�o
            // cliente.GenerarPedidoRandom();
        }
        else
        {
            Debug.Log("Entrega incorrecta");

            // penalizaci�n (bloquea cliente unos segundos)
            StartCoroutine(Penalizar());
        }

        sel.Limpiar();
    }

    bool ValidarEntrega(SeleccionActual sel)
    {
        // plato
        if (cliente.pedido.plato != sel.platoSeleccionado.tipo)
            return false;

        // bebida
        if (cliente.pedido.pideBebida)
        {
            if (!sel.tieneBebidaSeleccionada)
                return false;

            if (cliente.pedido.bebida != sel.bebidaSeleccionada)
                return false;

            if (cliente.pedido.tamañoBebida != sel.tamañoBebidaSeleccionada)
                return false;
        }

        // cremas: debe contener todas las del pedido
        if (cliente.pedido.cremas != null)
        {
            foreach (var c in cliente.pedido.cremas)
            {
                if (!sel.cremasSeleccionadas.Contains(c))
                    return false;
            }
        }

        return true;
    }

    IEnumerator Penalizar()
    {
        // aqu� puedes bloquear interacci�n visualmente si quieres
        yield return new WaitForSeconds(tiempoPenalizacion);

        // el cliente se va despu�s del error
        if (cliente != null)
        {
            cliente.pedido = null;
            Debug.Log("Cliente se fue por error");
        }
    }
}