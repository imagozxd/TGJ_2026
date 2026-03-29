using UnityEngine;

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

public class Ingredient : MonoBehaviour
{
    public IngredientType type = IngredientType.None;
}
