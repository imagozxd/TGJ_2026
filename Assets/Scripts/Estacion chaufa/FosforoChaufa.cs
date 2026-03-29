using UnityEngine;
using UnityEngine.UI;

public class FosforoChaufa : DraggableItem
{
    [SerializeField] private Sprite spriteAsignado;
    [SerializeField] private Vector2 escala = Vector2.one;

    protected override void ConfigurarGhost(GameObject ghost)
    {
        if (spriteAsignado == null) return;

        Image img = ghost.GetComponent<Image>();
        if (img == null) img = ghost.AddComponent<Image>();
        img.sprite = spriteAsignado;
        img.preserveAspect = true;

        ghost.transform.localScale = new Vector3(escala.x, escala.y, 1f);
    }
}
