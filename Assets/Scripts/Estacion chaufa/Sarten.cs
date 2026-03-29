using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Sarten : MonoBehaviour, IDropHandler
{
    public Flama flama;

    private HashSet<IngredientType> ingredientes = new HashSet<IngredientType>();

    public bool listoParaCocinar = false;

    //  NUEVO: guardar objetos usados
    private List<GameObject> ingredientesUsados = new List<GameObject>();

    public void OnDrop(PointerEventData eventData)
    {
        if (!flama.isOn)
        {
            Debug.Log("Flama apagada");
            return;
        }

        GameObject obj = eventData.pointerDrag;

        if (obj == null) return;

        Ingredient ingredient = obj.GetComponent<Ingredient>();
        if (ingredient == null) return;

        if (ingredient.type == IngredientType.Arroz) Add(obj, IngredientType.Arroz);
        if (ingredient.type == IngredientType.PolloTrozos) Add(obj, IngredientType.PolloTrozos);
        if (ingredient.type == IngredientType.Huevo) Add(obj, IngredientType.Huevo);
        if (ingredient.type == IngredientType.Cebolla) Add(obj, IngredientType.Cebolla);
        if (ingredient.type == IngredientType.Sillao) Add(obj, IngredientType.Sillao);

        CheckReceta();
    }

    void Add(GameObject obj, IngredientType ingredientType)
    {
        if (ingredientes.Contains(ingredientType))
            return;

        ingredientes.Add(ingredientType);
        ingredientesUsados.Add(obj);

        // limpiar ghost activo y deshabilitar drag
        DraggableItem dragItem = obj.GetComponent<DraggableItem>();
        if (dragItem != null)
        {
            dragItem.CleanupGhost();
            dragItem.fueEntregado = true;
            dragItem.enabled = false;
        }

        obj.transform.SetParent(transform, false);
        obj.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        //  bloquear interacci�n
        CanvasGroup cg = obj.GetComponent<CanvasGroup>();
        if (cg != null)
        {
            cg.blocksRaycasts = false;
            cg.interactable = false;
        }

        var drag = obj.GetComponent<DraggableItem>();
        if (drag != null) drag.enabled = false;
    }

    void CheckReceta()
    {
        bool completa =
            ingredientes.Contains(IngredientType.Arroz) &&
            ingredientes.Contains(IngredientType.PolloTrozos) &&
            ingredientes.Contains(IngredientType.Huevo) &&
            ingredientes.Contains(IngredientType.Cebolla) &&
            ingredientes.Contains(IngredientType.Sillao);

        if (completa)
        {
            listoParaCocinar = true;
            Debug.Log("RECETA COMPLETA");
        }
    }

    //  RESET DE ESTA ESTACI�N
    public void ResetEstacion()
    {
        Debug.Log(" Reset Chaufa");

        foreach (var obj in ingredientesUsados)
        {
            InitialTransform init = obj.GetComponent<InitialTransform>();

            if (init == null)
            {
                Debug.LogWarning(" Falta InitialTransform en: " + obj.name);
                continue;
            }

            //  volver al parent original
            obj.transform.SetParent(init.initialParent, false);

            //  restaurar posici�n UI
            RectTransform rect = obj.GetComponent<RectTransform>();
            rect.anchoredPosition = init.initialPosition;

            //  asegurar que se vea (no quede detr�s)
            obj.transform.SetAsLastSibling();

            //  reactivar interacci�n
            CanvasGroup cg = obj.GetComponent<CanvasGroup>();
            if (cg != null)
            {
                cg.blocksRaycasts = true;
                cg.interactable = true;
                cg.alpha = 1f;
            }

            //  reactivar drag
            var drag = obj.GetComponent<DraggableItem>();
            if (drag != null)
            {
                drag.enabled = true;
            }

            Debug.Log($" Reset: {obj.name} > {init.initialPosition}");
        }

        //  limpiar estado
        ingredientes.Clear();
        ingredientesUsados.Clear();
        listoParaCocinar = false;
    }
}