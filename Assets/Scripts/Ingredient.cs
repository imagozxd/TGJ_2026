using UnityEngine;
using UnityEngine.UI;

public enum IngredientType
{
    None = 0,
    Arroz,
    PolloTrozos,
    Huevo,
    Cebolla,
    Sillao,
    Chaufa,
}

public class Ingredient : DraggableItem
{
    public IngredientType type = IngredientType.None;

    [SerializeField] private Sprite spriteAsignado;
    [SerializeField] private Vector2 escala = Vector2.one;

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

        ghost.transform.localScale = new Vector3(escala.x, escala.y, 1f);
    }
}
