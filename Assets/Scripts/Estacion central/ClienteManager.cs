using UnityEngine;

public class ClienteManager : MonoBehaviour
{
    public ClientePedido[] slots; // ClienteSlot_1, ClienteSlot_2

    public void ClienteLlego()
    {
        Debug.Log("Intentando asignar cliente a slot");

        for (int i = 0; i < slots.Length; i++)
        {
            if (!slots[i].TienePedido())
            {
                slots[i].GenerarPedidoRandom();

                Debug.Log("Cliente asignado a slot " + i);
                return;
            }
        }

        // si no hay espacio > pierdes
        Debug.Log("No hay slots disponibles > GAME OVER");
    }
}