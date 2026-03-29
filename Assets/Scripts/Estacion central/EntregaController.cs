using UnityEngine;
using System.Collections.Generic;

public class EntregaController : MonoBehaviour
{
    public void Entregar(
        ClientePedido cliente,
        TipoPlato plato,
        Bebida bebida,
        List<Crema> cremas
    )
    {
        bool correcto = true;

        if (cliente.pedido.plato != plato)
            correcto = false;

        if (cliente.pedido.bebida != bebida)
            correcto = false;

        foreach (var c in cliente.pedido.cremas)
        {
            if (!cremas.Contains(c))
                correcto = false;
        }

        if (correcto)
        {
            Debug.Log("Entrega correcta");
            Destroy(cliente.gameObject);
        }
        else
        {
            Debug.Log("Entrega incorrecta");

            // penalizaci�n
            StartCoroutine(Penalizar(cliente));
        }
    }

    System.Collections.IEnumerator Penalizar(ClientePedido cliente)
    {
        yield return new WaitForSeconds(2f);

        Destroy(cliente.gameObject);
    }
}