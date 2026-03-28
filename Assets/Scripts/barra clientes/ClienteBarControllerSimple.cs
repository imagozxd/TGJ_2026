using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClienteBarControllerSimple : MonoBehaviour
{
    public static ClienteBarControllerSimple Instance;

    public Transform puntoA;
    public Transform puntoB;

    public GameObject clientePrefab;
    public Transform container;

    // rango de tiempo entre spawns
    public float minSpawnInterval = 2f;
    public float maxSpawnInterval = 5f;

    public float speed = 100f;

    // conexión con manager de slots
    public ClienteManager manager;

    private List<ClienteMover> clientes = new List<ClienteMover>();

    private int clientesEnLocal = 0;

    private void Awake()
    {
        Instance = this;
    }

    // inicia el sistema de spawn
    public void StartGame()
    {
        StartCoroutine(SpawnLoop());
    }

    // loop que genera clientes con tiempo aleatorio
    IEnumerator SpawnLoop()
    {
        while (true)
        {
            SpawnCliente();

            float waitTime = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(waitTime);
        }
    }

    // crea un cliente en A y lo envía a B
    public void SpawnCliente()
    {
        if (clientePrefab == null || puntoA == null || puntoB == null)
        {
            Debug.LogError("Faltan referencias en ClienteBarControllerSimple");
            return;
        }

        GameObject go = Instantiate(clientePrefab, container);

        // posicion inicial
        go.transform.position = puntoA.position;

        ClienteMover mover = go.GetComponent<ClienteMover>();

        if (mover == null)
        {
            Debug.LogError("ClienteMover no encontrado en prefab");
            return;
        }

        // inicializa movimiento hacia B
        mover.Init(puntoB.position, speed);

        clientes.Add(mover);
    }

    // llamado cuando el cliente llega a B
    public void ClienteLlego(ClienteMover cliente)
    {
        clientesEnLocal++;

        Debug.Log("Cliente en local: " + clientesEnLocal);

        // intentar asignar a un slot de pedidos
        if (manager != null)
        {
            manager.ClienteLlego();
        }
        else
        {
            Debug.LogWarning("ClienteManager no asignado");
        }

        // condición de derrota
        if (clientesEnLocal > 2)
        {
            Debug.Log("GAME OVER");
        }
    }

    // llamado cuando se atiende correctamente un cliente
    public void AtenderCliente()
    {
        for (int i = 0; i < clientes.Count; i++)
        {
            if (clientes[i].enLocal)
            {
                Destroy(clientes[i].gameObject);
                clientes.RemoveAt(i);

                clientesEnLocal--;

                Debug.Log("Cliente atendido");
                return;
            }
        }
    }

    // llamado cuando un cliente se va (correcto o error)
    public void ClienteSale()
    {
        clientesEnLocal--;

        if (clientesEnLocal < 0)
            clientesEnLocal = 0;

        Debug.Log("Cliente sale. En local: " + clientesEnLocal);
    }
}