using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClienteBarUI : MonoBehaviour
{
    public ClienteBarController controller;

    public RectTransform container;
    public GameObject clientePrefab;

    public Sprite[] spritesClientes;

    public float spacing = 120f;
    public float speed = 5f;

    private List<GameObject> clientesUI = new List<GameObject>();

    void Update()
    {
        UpdatePositions();
    }

    public void RefreshUI()
    {
        // crear solo los que faltan
        while (clientesUI.Count < controller.clientes.Count)
        {
            GameObject go = Instantiate(clientePrefab, container);
            clientesUI.Add(go);
        }

        // eliminar extras
        while (clientesUI.Count > controller.clientes.Count)
        {
            Destroy(clientesUI[0]);
            clientesUI.RemoveAt(0);
        }

        // actualizar datos (sprite/color)
        for (int i = 0; i < controller.clientes.Count; i++)
        {
            Cliente cliente = controller.clientes[i];
            GameObject go = clientesUI[i];

            Image img = go.GetComponent<Image>();

            img.sprite = spritesClientes[(int)cliente.tipo];

            if (cliente.estado == ClienteEstado.EnLocal)
                img.color = Color.red;
            else
                img.color = Color.yellow;
        }
    }

    void UpdatePositions()
    {
        for (int i = 0; i < clientesUI.Count; i++)
        {
            RectTransform rect = clientesUI[i].GetComponent<RectTransform>();

            Vector2 target = new Vector2(-i * spacing, 0);

            rect.anchoredPosition = Vector2.Lerp(
                rect.anchoredPosition,
                target,
                Time.deltaTime * speed
            );
        }
    }
}