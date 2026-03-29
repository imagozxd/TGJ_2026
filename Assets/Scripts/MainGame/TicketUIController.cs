using UnityEngine;
using UnityEngine.UI;

public class TicketUICOntroller : MonoBehaviour
{
    [SerializeField] private GameObject checkGO;
    [Header("Bebidas")]
    [SerializeField] private GameObject gaseosa1LTGO;
    [SerializeField] private GameObject gaseosaMediaGO;
    [SerializeField] private GameObject gaseosaPersonalGO;

    [Header("Platos")]
    [SerializeField] private GameObject polloGO;
    [SerializeField] private GameObject chaufaGO;

    [Header("Cremas")]

    [SerializeField] private GameObject mayonesaGO;
    [SerializeField] private GameObject mostazaGO;
    [SerializeField] private GameObject ketchupGO;

    [Header("Sprites")]
    [SerializeField] private Texture coca1LTSPR;
    [SerializeField] private Texture cocaMediaSPR;
    [SerializeField] private Texture cocaPersonalSPR;
    [SerializeField] private Texture inka1LTSPR;
    [SerializeField] private Texture inkaMediaSPR;
    [SerializeField] private Texture inkaPersonalSPR;
    [SerializeField] private Texture sprite1LTSPR;
    [SerializeField] private Texture spriteMediaSPR;
    [SerializeField] private Texture spritePersonalSPR;

    private ClientePedido clientePedido;

    void Start()
    {
        checkGO.SetActive(false);
    }

    public void Init(ClientePedido newClientePedido)
    {
        clientePedido = newClientePedido;
        checkGO.SetActive(false);

        if (clientePedido.pedido.plato == TipoPlato.Pollo)
        {
            polloGO.SetActive(true);
            chaufaGO.SetActive(false);
        }
        else
        {
            polloGO.SetActive(false);
            chaufaGO.SetActive(true);
        }

        // Bebidas
        if (clientePedido.pedido.pideBebida)
        {
            switch (clientePedido.pedido.bebida)
            {
                case Bebida.Coca:
                    gaseosa1LTGO.GetComponentInChildren<RawImage>().texture = coca1LTSPR;
                    gaseosaMediaGO.GetComponentInChildren<RawImage>().texture = cocaMediaSPR;
                    gaseosaPersonalGO.GetComponentInChildren<RawImage>().texture = cocaPersonalSPR;
                    break;
                case Bebida.Inca:
                    gaseosa1LTGO.GetComponentInChildren<RawImage>().texture = inka1LTSPR;
                    gaseosaMediaGO.GetComponentInChildren<RawImage>().texture = inkaMediaSPR;
                    gaseosaPersonalGO.GetComponentInChildren<RawImage>().texture = inkaPersonalSPR;
                    break;
                case Bebida.Sprite:
                    gaseosa1LTGO.GetComponentInChildren<RawImage>().texture = sprite1LTSPR;
                    gaseosaMediaGO.GetComponentInChildren<RawImage>().texture = spriteMediaSPR;
                    gaseosaPersonalGO.GetComponentInChildren<RawImage>().texture = spritePersonalSPR;
                    break;
            }

            // TODO: AQuí dependiendo del tamaño de la gaseosa se activa el GO correspondiente
            switch (clientePedido.pedido.tamañoBebida)
            {
                case TamañoBebida.Chico:
                    gaseosa1LTGO.SetActive(false);
                    gaseosaMediaGO.SetActive(false);
                    gaseosaPersonalGO.SetActive(true);
                    break;
                case TamañoBebida.Mediano:
                    gaseosa1LTGO.SetActive(false);
                    gaseosaMediaGO.SetActive(true);
                    gaseosaPersonalGO.SetActive(false);
                    break;
                case TamañoBebida.Grande:
                    gaseosa1LTGO.SetActive(true);
                    gaseosaMediaGO.SetActive(false);
                    gaseosaPersonalGO.SetActive(false);
                    break;
            }
        }
        else
        {
            gaseosa1LTGO.SetActive(false);
            gaseosaMediaGO.SetActive(false);
            gaseosaPersonalGO.SetActive(false);
        }

        // Cremas
        if (clientePedido.pedido.cremas != null)
        {
            mayonesaGO.SetActive(clientePedido.pedido.cremas.Contains(Crema.Mayo));
            mostazaGO.SetActive(clientePedido.pedido.cremas.Contains(Crema.Mostaza));
            ketchupGO.SetActive(clientePedido.pedido.cremas.Contains(Crema.Ketchup));
        }
        else
        {
            mayonesaGO.SetActive(false);
            mostazaGO.SetActive(false);
            ketchupGO.SetActive(false);
        }
    }

    public bool EsClientePedido(ClientePedido newClientePedido)
    {
        return clientePedido == newClientePedido;
    }

    public void MarkAsReady()
    {
        checkGO.SetActive(true);
    }


}
