using UnityEngine;

public class ClienteMover : MonoBehaviour
{
    public float speed;
    private Vector3 target;

    public bool enLocal = false;

    public void Init(Vector3 targetPos, float moveSpeed)
    {
        target = targetPos;
        speed = moveSpeed;
    }

    void Update()
    {
        if (enLocal) return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            speed * Time.deltaTime
        );

        // llegó a B
        if (Vector3.Distance(transform.position, target) < 1f)
        {
            enLocal = true;
            ClienteBarControllerSimple.Instance.ClienteLlego(this);
        }
    }
}