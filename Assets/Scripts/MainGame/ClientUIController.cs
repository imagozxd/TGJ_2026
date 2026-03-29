using UnityEngine;

public class ClientUIController : MonoBehaviour
{
    [SerializeField] private GameObject susyDiazGO;
    [SerializeField] private GameObject dinaGO;
    [SerializeField] private GameObject zumbaGO;

    private ClientePedido clientePedido;

    public void Init(ClientePedido newClientePedido)
    {
        Debug.Log($"[ClientUIController] Mostrando cliente de tipo {newClientePedido.cliente.tipo}");

        clientePedido = newClientePedido;

        susyDiazGO.SetActive(false);
        dinaGO.SetActive(false);
        zumbaGO.SetActive(false);

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
