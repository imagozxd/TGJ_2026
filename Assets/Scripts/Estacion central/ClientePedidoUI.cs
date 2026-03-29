using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ClientePedidoUI : MonoBehaviour
{
    public ClientePedido cliente;

    [Header("UI")]
    public Image iconoPlato;
    public Image iconoBebida;
    public Transform contenedorCremas;
    public GameObject cremaPrefab;

    [Header("Sprites")]
    public Sprite[] spritesPlato;   // Pollo, Chaufa
    public Sprite[] spritesBebida;  // Coca, Inca, Sprite
    public Sprite[] spritesCremas;  // Mayo, Mostaza, Ketchup

    public void RefreshUI()
    {
        if (cliente == null || cliente.pedido == null)
        {
            Debug.Log("No hay pedido para mostrar");
            return;
        }

        var pedido = cliente.pedido;

        // plato
        iconoPlato.sprite = spritesPlato[(int)pedido.plato];

        // bebida
        iconoBebida.sprite = spritesBebida[(int)pedido.bebida];

        // limpiar cremas anteriores
        foreach (Transform child in contenedorCremas)
            Destroy(child.gameObject);

        // crear cremas
        foreach (var crema in pedido.cremas)
        {
            GameObject go = Instantiate(cremaPrefab, contenedorCremas);
            Image img = go.GetComponent<Image>();
            img.sprite = spritesCremas[(int)crema];
        }

        Debug.Log("UI Pedido actualizada");
    }
}