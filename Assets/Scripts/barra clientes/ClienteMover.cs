using UnityEngine;
using UnityEngine.UI;

public class ClienteMover : MonoBehaviour
{

    [SerializeField] private Image indicatorIMG;
    [SerializeField] private Sprite susyDiazFaceSPR;
    [SerializeField] private Sprite dinaFaceSPR;
    [SerializeField] private Sprite zumbaFaceSPR;

    public float speed;
    private Vector3 target;

    public bool enLocal = false;
    Cliente cliente;

    public void Init(Vector3 targetPos, float moveSpeed, Cliente newCliente)
    {
        target = targetPos;
        speed = moveSpeed;
        cliente = newCliente;

        if (cliente.tipo == TipoCliente.SusyDiaz)
        {
            indicatorIMG.sprite = susyDiazFaceSPR;
        }
        else if (cliente.tipo == TipoCliente.Dina)
        {
            indicatorIMG.sprite = dinaFaceSPR;
        }
        else if (cliente.tipo == TipoCliente.Zumba)
        {
            indicatorIMG.sprite = zumbaFaceSPR;
        }
    }

    void Update()
    {
        if (enLocal) return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            speed * Time.deltaTime
        );

        // lleg� a B
        if (Vector3.Distance(transform.position, target) < 1f)
        {
            enLocal = true;
            ClienteBarControllerSimple.Instance.ClienteLlego(this);
        }
    }

    public Cliente GetCliente()
    {
        return cliente;
    }
}