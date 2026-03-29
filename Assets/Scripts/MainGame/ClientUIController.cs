using UnityEngine;

public class ClientUIController : MonoBehaviour
{
    [SerializeField] private GameObject susyDiazGO;
    [SerializeField] private GameObject dinaGO;
    [SerializeField] private GameObject zumbaGO;

    private ClientePedido clientePedido;

    public void Init(ClientePedido newClientePedido)
    {
        clientePedido = newClientePedido;
        switch (newClientePedido.cliente.tipo)
        {
            case TipoCliente.SusyDiaz:
                susyDiazGO.SetActive(true);
                break;
            case TipoCliente.Dina:
                dinaGO.SetActive(true);
                break;
            case TipoCliente.Zumba:
                zumbaGO.SetActive(true);
                break;
        }
    }

    public bool EsCliente(ClientePedido clientePedido)
    {
        return this.clientePedido == clientePedido;
    }
}
