using UnityEngine;

public class ClientController : MonoBehaviour
{
    private Pedido pedido = null;

    public void SetRandomPedido()
    {
        // Logica para preparar el pedido a realizar

    }

    public void HacerPedido()
    {
        PlayerManager.Instance.EncolarPedido(pedido);
    }
}
