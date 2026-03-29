using UnityEngine;
using UnityEngine.UI;

public class CremaItem : DraggableItem
{
    [SerializeField] private Crema tipo;
    [SerializeField] private Sprite spriteAsignado;

    public Crema Tipo    => tipo;
    public Sprite Sprite => spriteAsignado;
    public Vector3 Escala => new Vector3(0.87f, 1.35f, 1f);

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

        ghost.transform.localScale = new Vector3(0.87f, 1.35f, 1f);
    }
}