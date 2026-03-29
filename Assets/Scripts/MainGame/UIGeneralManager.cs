using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class UIGeneralManager : MonoBehaviour
{
    private static UIGeneralManager Instance { get; set; }

    [SerializeField] private GameObject ticketUIParentGO;

    [SerializeField] private GameObject ticketUIPrefab;
    private List<GameObject> activeTicketsGO = new();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    

    public void AddTicket(Pedido pedido)
    {
        GameObject newTicketGO = Instantiate(ticketUIPrefab, ticketUIParentGO.transform);
        activeTicketsGO.Add(newTicketGO);
        
    }

    public void MarkTicketAsReady(Pedido pedido)
    {
        // buscar el ticket correspondiente y marcarlo como listo
        foreach (GameObject ticketGO in activeTicketsGO)
        {
            TicketUICOntroller controller = ticketGO.GetComponent<TicketUICOntroller>();
            if (controller != null && controller.EsPedido(pedido))
            {
                Debug.Log($"[UIGeneralManager] Marcando ticket como listo: {pedido.plato} con bebida {pedido.bebida}");
                StartCoroutine(MarkAsReadyCoroutine(controller, ticketGO));
                break;
            }
        }
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
