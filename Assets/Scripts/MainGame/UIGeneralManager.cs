using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class UIGeneralManager : MonoBehaviour
{
    public static UIGeneralManager Instance { get; set; }

    [SerializeField] private GameObject ticketUIParentGO;

    [SerializeField] private GameObject ticketUIPrefab;
    private List<GameObject> activeTicketsGO = new();

    [SerializeField] private GameObject clienteLeftPosGO;
    [SerializeField] private GameObject clienteRightPosGO;
    [SerializeField] private GameObject clienteCenterPosGO;
    [SerializeField] private GameObject clientePrefab;
    private List<GameObject> activeClientesGO = new();


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }


    public void ProcessNewClientePedido(ClientePedido newClientePedido)
    {
        Debug.Log($"[UIGeneralManager] Procesando nuevo pedido: {newClientePedido.pedido.plato} con bebida {newClientePedido.pedido.bebida}");
        
        bool clienteMostrado = ShowCliente(newClientePedido);
        if (clienteMostrado) {
            AddTicket(newClientePedido);
        }
        else
        {
            Debug.LogWarning($"[UIGeneralManager] No se pudo mostrar el cliente para el pedido: {newClientePedido.pedido.plato} con bebida {newClientePedido.pedido.bebida}");
        }
    }

    private void AddTicket(ClientePedido newClientePedido)
    {
        GameObject newTicketGO = Instantiate(ticketUIPrefab, ticketUIParentGO.transform);
        newTicketGO.GetComponent<TicketUICOntroller>().Init(newClientePedido);
        activeTicketsGO.Add(newTicketGO);
    }

    private bool ShowCliente(ClientePedido newClientePedido)
    {
        Transform selectedParentTF = null;

        // Busca una posición libre para el nuevo cliente
        Transform[] possibleParents = new Transform[] { clienteLeftPosGO.transform, clienteCenterPosGO.transform, clienteRightPosGO.transform };
        foreach (Transform parent in possibleParents)
        {
            if (parent.childCount == 0)
            {
                selectedParentTF = parent;
                break;
            }
        }

        // Si la encuentra, genera un nuevo GameObject ClienteUI y lo inicializa
        if (selectedParentTF != null)
        {
            GameObject newClientGO = Instantiate(clientePrefab, selectedParentTF);
            newClientGO.GetComponent<ClientUIController>().Init(newClientePedido);
            activeClientesGO.Add(newClientGO);
        }
        else
        {
            Debug.LogWarning($"[UIGeneralManager] No hay espacio para mostrar el cliente: {newClientePedido.cliente.tipo.ToString()}");
        }

        return selectedParentTF != null;    
    }

    public void ProcessClientePedidoCompleted(ClientePedido completedPedido)
    {
        Debug.Log($"[UIGeneralManager] Pedido completado: {completedPedido.pedido.plato} con bebida {completedPedido.pedido.bebida}");
        MarkTicketAsReady(completedPedido);
        RemoveCliente(completedPedido);
    }

    public void MarkTicketAsReady(ClientePedido newClientePedido)
    {
        // buscar el ticket correspondiente y marcarlo como listo
        foreach (GameObject ticketGO in activeTicketsGO)
        {
            TicketUICOntroller controller = ticketGO.GetComponent<TicketUICOntroller>();
            if (controller != null && controller.EsClientePedido(newClientePedido))
            {
                Debug.Log($"[UIGeneralManager] Marcando ticket como listo: {newClientePedido.pedido.plato} con bebida {newClientePedido.pedido.bebida}");
                StartCoroutine(MarkAsReadyCoroutine(controller, ticketGO));
                break;
            }
        }
    }

    private void RemoveCliente(ClientePedido clientePedido)
    {
        // buscar el cliente correspondiente y eliminarlo
        foreach (GameObject clienteGO in activeClientesGO)
        {
            ClientUIController controller = clienteGO.GetComponent<ClientUIController>();
            if (controller != null && controller.EsCliente(clientePedido))
            {
                Debug.Log($"[UIGeneralManager] Eliminando cliente: {clientePedido.cliente.tipo.ToString()}");
                StartCoroutine(RemoveClienteCoroutine(clienteGO));
                break;
            }
        }
    }

    private IEnumerator RemoveClienteCoroutine(GameObject clienteGO)
    {
        // Animación de fade out
        CanvasGroup canvasGroup = clienteGO.GetComponent<CanvasGroup>();
        if (canvasGroup != null)
        {
            float duration = 0.5f;
            float startAlpha = canvasGroup.alpha;
            float endAlpha = 0f;
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
                yield return null;
            }

            canvasGroup.alpha = endAlpha;
        }

        activeClientesGO.Remove(clienteGO);
        Destroy(clienteGO);
    }

    private IEnumerator MarkAsReadyCoroutine(TicketUICOntroller controller, GameObject ticketGO)
    {
        controller.MarkAsReady();

        // Pequeña animación de fade out
        CanvasGroup canvasGroup = ticketGO.GetComponent<CanvasGroup>();
        if (canvasGroup != null)
        {
            float duration = 0.5f;
            float startAlpha = canvasGroup.alpha;
            float endAlpha = 0f;
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
                yield return null;
            }

            canvasGroup.alpha = endAlpha;
        }

        activeTicketsGO.Remove(ticketGO);
        Destroy(ticketGO);
    }
}
