using UnityEngine;

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
    [SerializeField] private Sprite coca1LTSPR;
    [SerializeField] private Sprite cocaMediaSPR;
    [SerializeField] private Sprite cocaPersonalSPR;
    [SerializeField] private Sprite inka1LTSPR;
    [SerializeField] private Sprite inkaMediaSPR;
    [SerializeField] private Sprite inkaPersonalSPR;

    [SerializeField] private Sprite sprite1LTSPR;
    [SerializeField] private Sprite spriteMediaSPR;
    [SerializeField] private Sprite spritePersonalSPR;

    private Pedido pedido;

    void Start()
    {
        checkGO.SetActive(false);
    }

    public void Init(Pedido pedido)
    {
        this.pedido = pedido;
        checkGO.SetActive(false);

        if (pedido.plato == TipoPlato.Pollo)
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
        if (pedido.pideBebida)
        {
            switch (pedido.bebida)
            {
                case Bebida.Coca:
                    gaseosa1LTGO.GetComponent<SpriteRenderer>().sprite = coca1LTSPR;
                    gaseosaMediaGO.GetComponent<SpriteRenderer>().sprite = cocaMediaSPR;
                    gaseosaPersonalGO.GetComponent<SpriteRenderer>().sprite = cocaPersonalSPR;
                    break;
                case Bebida.Inca:
                    gaseosa1LTGO.GetComponent<SpriteRenderer>().sprite = inka1LTSPR;
                    gaseosaMediaGO.GetComponent<SpriteRenderer>().sprite = inkaMediaSPR;
                    gaseosaPersonalGO.GetComponent<SpriteRenderer>().sprite = inkaPersonalSPR;
                    break;
                case Bebida.Sprite:
                    gaseosa1LTGO.GetComponent<SpriteRenderer>().sprite = sprite1LTSPR;
                    gaseosaMediaGO.GetComponent<SpriteRenderer>().sprite = spriteMediaSPR;
                    gaseosaPersonalGO.GetComponent<SpriteRenderer>().sprite = spritePersonalSPR;
                    break;
            }

            // TODO: AQuí dependiendo del tamaño de la gaseosa se activa el GO correspondiente
        }
        else
        {
            gaseosa1LTGO.SetActive(false);
            gaseosaMediaGO.SetActive(false);
            gaseosaPersonalGO.SetActive(false);
        }

        // Cremas
        if (pedido.cremas != null)
        {
            mayonesaGO.SetActive(pedido.cremas.Contains(Crema.Mayo));
            mostazaGO.SetActive(pedido.cremas.Contains(Crema.Mostaza));
            ketchupGO.SetActive(pedido.cremas.Contains(Crema.Ketchup));
        }
        else
        {
            mayonesaGO.SetActive(false);
            mostazaGO.SetActive(false);
            ketchupGO.SetActive(false);
        }
    }

    public bool EsPedido(Pedido pedido)
    {
        return this.pedido == pedido;
    }

    public void MarkAsReady()
    {
        checkGO.SetActive(true);
    }


}
