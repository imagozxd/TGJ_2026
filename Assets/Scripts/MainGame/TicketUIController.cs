using UnityEngine;
using UnityEngine.UI;

public class TicketUICOntroller : MonoBehaviour
{
    [SerializeField] private Image clientFaceIMG;
    [SerializeField] private Image checkIMG;

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
    [SerializeField] private Sprite coca1LTSPR;
    [SerializeField] private Sprite cocaMediaSPR;
    [SerializeField] private Sprite cocaPersonalSPR;
    [SerializeField] private Sprite inka1LTSPR;
    [SerializeField] private Sprite inkaMediaSPR;
    [SerializeField] private Sprite inkaPersonalSPR;
    [SerializeField] private Sprite sprite1LTSPR;
    [SerializeField] private Sprite spriteMediaSPR;
    [SerializeField] private Sprite spritePersonalSPR;
    [SerializeField] private Sprite checkSPR;
    [SerializeField] private Sprite noCheckSPR;


    [Space]
    [SerializeField] private Sprite susyDiazFaceSPR;
    [SerializeField] private Sprite dinaFaceSPR;
    [SerializeField] private Sprite zumbaFaceSPR;

    private ClientePedido clientePedido;


    public void Init(ClientePedido newClientePedido)
    {
        clientePedido = newClientePedido;
        checkIMG.sprite = noCheckSPR;

        if (newClientePedido.cliente.tipo == TipoCliente.SusyDiaz)
        {
            clientFaceIMG.sprite = susyDiazFaceSPR;
        }
        else if (newClientePedido.cliente.tipo == TipoCliente.Dina)
        {
            clientFaceIMG.sprite = dinaFaceSPR;
        }
        else if (newClientePedido.cliente.tipo == TipoCliente.Zumba)
        {
            clientFaceIMG.sprite = zumbaFaceSPR;
        }
        else
        {
            Debug.LogWarning($"[TicketUICOntroller] Tipo de cliente desconocido: {newClientePedido.cliente.tipo}");
        }

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
                    gaseosa1LTGO.GetComponent<Image>().sprite = coca1LTSPR;
                    gaseosaMediaGO.GetComponent<Image>().sprite = cocaMediaSPR;
                    gaseosaPersonalGO.GetComponent<Image>().sprite = cocaPersonalSPR;
                    break;
                case Bebida.Inca:
                    gaseosa1LTGO.GetComponent<Image>().sprite = inka1LTSPR;
                    gaseosaMediaGO.GetComponent<Image>().sprite = inkaMediaSPR;
                    gaseosaPersonalGO.GetComponent<Image>().sprite = inkaPersonalSPR;
                    break;
                case Bebida.Sprite:
                    gaseosa1LTGO.GetComponent<Image>().sprite = sprite1LTSPR;
                    gaseosaMediaGO.GetComponent<Image>().sprite = spriteMediaSPR;
                    gaseosaPersonalGO.GetComponent<Image>().sprite = spritePersonalSPR;
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
        checkIMG.sprite = checkSPR;
    }


}
