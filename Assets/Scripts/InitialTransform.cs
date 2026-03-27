using UnityEngine;

public class InitialTransform : MonoBehaviour
{
    public Transform initialParent;
    public Vector2 initialPosition;

    void Start()
    {
        initialParent = transform.parent;
        initialPosition = GetComponent<RectTransform>().anchoredPosition;

        Debug.Log("INIT OK: " + gameObject.name + " pos: " + initialPosition);
    }
}