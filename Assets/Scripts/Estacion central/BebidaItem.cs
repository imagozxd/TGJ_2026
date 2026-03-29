using UnityEngine;
using UnityEngine.UI;

public class BebidaItem : DraggableItem
{
    [SerializeField] private Bebida tipo;
    [SerializeField] private TamañoBebida tamaño;
    [SerializeField] private Sprite spriteAsignado;

    public Bebida Tipo         => tipo;
    public TamañoBebida Tamaño => tamaño;
    public Sprite Sprite       => spriteAsignado;
    public Vector3 Escala => tamaño switch
    {
        TamañoBebida.Chico   => new Vector3(0.6f,  1.98f, 1f),
        TamañoBebida.Mediano => new Vector3(1f,    3.1f,  1f),
        TamañoBebida.Grande  => new Vector3(1.2f,  3.8f,  1f),
        _                    => Vector3.one
    };

    protected override void ConfigurarGhost(GameObject ghost)
    {
        Image image = ghost.GetComponent<Image>();
        if (image != null && spriteAsignado != null)
        {
            image.sprite = spriteAsignado;
            Color c = image.color;
            c.a = 1f;
            image.color = c;
        }

        ghost.transform.localScale = tamaño switch
        {
            TamañoBebida.Chico   => new Vector3(0.6f,  1.98f, 1f),
            TamañoBebida.Mediano => new Vector3(1f,    3.1f,  1f),
            TamañoBebida.Grande  => new Vector3(1.2f,  3.8f,  1f),
            _                    => Vector3.one
        };
    }
}