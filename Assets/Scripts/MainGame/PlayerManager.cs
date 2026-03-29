using UnityEngine;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }
    List<Pedido> pedidosEnCurso = new();


    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void EncolarPedido(Pedido pedido)
    {
        pedidosEnCurso.Add(pedido);
        // Modificar UI aquí
    }

    public void CompletarPedido(Pedido pedido)
    {
        if (pedidosEnCurso.Contains(pedido))
        {
            pedidosEnCurso.Remove(pedido);
            Debug.Log("[PlayerManager] Pedido completado: " + pedido);
        }
        else
        {
            Debug.LogWarning("[PlayerManager] Pedido no encontrado en curso: " + pedido);
        }
    }
}