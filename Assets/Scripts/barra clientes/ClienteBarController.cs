using System.Collections.Generic;
using UnityEngine;

public class ClienteBarController : MonoBehaviour
{
    public List<Cliente> clientes = new List<Cliente>();

    public int maxEnLocal = 2;

    private void Start()
    {
        Inicializar();
    }

    void Inicializar()
    {
        clientes.Clear();

        // primer cliente acercándose
        clientes.Add(new Cliente
        {
            estado = ClienteEstado.Acercandose,
            tipo = GetRandomTipo()
        });

        DebugEstado();
    }

    TipoCliente GetRandomTipo()
    {
        return (TipoCliente)Random.Range(0, 3);
    }

    public void UpdateBar()
    {
        // mover acercándose > en local
        foreach (var c in clientes)
        {
            if (c.estado == ClienteEstado.Acercandose)
            {
                c.estado = ClienteEstado.EnLocal;
                break;
            }
        }

        // contar en local
        int enLocal = 0;
        foreach (var c in clientes)
        {
            if (c.estado == ClienteEstado.EnLocal)
                enLocal++;
        }

        // condición de derrota
        if (enLocal > maxEnLocal)
        {
            Debug.Log("Game Over");
            return;
        }

        // agregar nuevo cliente acercándose
        clientes.Add(new Cliente
        {
            estado = ClienteEstado.Acercandose,
            tipo = GetRandomTipo()
        });

        DebugEstado();
    }

    public void AtenderCliente()
    {
        for (int i = 0; i < clientes.Count; i++)
        {
            if (clientes[i].estado == ClienteEstado.EnLocal)
            {
                clientes.RemoveAt(i);
                break;
            }
        }

        Debug.Log("Cliente atendido");
        DebugEstado();
    }

    void DebugEstado()
    {
        string log = "Clientes: ";

        foreach (var c in clientes)
        {
            log += $"[{c.tipo} - {c.estado}] ";
        }

        Debug.Log(log);
    }
}