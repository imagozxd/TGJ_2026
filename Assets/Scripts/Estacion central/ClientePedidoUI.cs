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

        if (iconoPlato != null && spritesPlato != null && (int)pedido.plato >= 0 && (int)pedido.plato < spritesPlato.Length)
            iconoPlato.sprite = spritesPlato[(int)pedido.plato];

        if (iconoBebida != null)
        {
            iconoBebida.gameObject.SetActive(pedido.pideBebida);
            if (pedido.pideBebida && spritesBebida != null && (int)pedido.bebida >= 0 && (int)pedido.bebida < spritesBebida.Length)
                iconoBebida.sprite = spritesBebida[(int)pedido.bebida];
        }

        if (contenedorCremas != null)
        {
            bool pideCremas = pedido.cremas != null && pedido.cremas.Count > 0;
            contenedorCremas.gameObject.SetActive(pideCremas);

            // limpiar cremas anteriores
            foreach (Transform child in contenedorCremas)
                Destroy(child.gameObject);

            // crear cremas
            if (pideCremas)
            {
                foreach (var crema in pedido.cremas)
                {
                    GameObject go = Instantiate(cremaPrefab, contenedorCremas);
                    Image img = go.GetComponent<Image>();
                    if (img != null && spritesCremas != null && (int)crema >= 0 && (int)crema < spritesCremas.Length)
                        img.sprite = spritesCremas[(int)crema];
                }
            }
        }

        Debug.Log("UI Pedido actualizada");
    }
}