using UnityEngine;
using UnityEngine.UI;

public class Pollo : MonoBehaviour
{
    public enum Estado
    {
        Crudo,
        Cocinando,
        Cocido,
        Quemado
    }

    public Estado estado = Estado.Crudo;

    public float tiempoCoccion = 3f;
    public float tiempoQuemado = 5f;

    private float timer = 0f;
    private bool isCooking = false;

    [Header("Sprites")]
    public Sprite crudo;
    public Sprite cocido;
    public Sprite quemado;

    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void StartCooking()
    {
        estado = Estado.Cocinando;
        isCooking = true;
        timer = 0f;
    }

    public void StopCooking()
    {
        isCooking = false;

        //  importante: si ya estaba cocido, se queda así
        if (estado == Estado.Cocinando)
        {
            estado = Estado.Crudo;
            image.sprite = crudo;
        }

        Debug.Log("Cocción detenida");
    }

    private void Update()
    {
        if (!isCooking) return;

        timer += Time.deltaTime;

        if (estado == Estado.Cocinando && timer >= tiempoCoccion)
        {
            estado = Estado.Cocido;
            image.sprite = cocido;

            Debug.Log("Pollo cocido");
        }

        if (timer >= tiempoQuemado)
        {
            estado = Estado.Quemado;
            image.sprite = quemado;

            Debug.Log("Pollo quemado");
        }
    }
    public bool EsServible()
    {
        return estado == Estado.Cocido || estado == Estado.Quemado;
    }
    public void ResetPollo()
    {
        estado = Estado.Crudo;
        StopCooking();

        // opcional: sprite crudo
        Debug.Log("Pollo reseteado");
    }
}