using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Sarten : MonoBehaviour, IDropHandler
{
    public Flama flama;

    private HashSet<System.Type> ingredientes = new HashSet<System.Type>();

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

        if (obj.GetComponent<Arroz>() != null) Add<Arroz>(obj);
        if (obj.GetComponent<PolloTrozos>() != null) Add<PolloTrozos>(obj);
        if (obj.GetComponent<Huevo>() != null) Add<Huevo>(obj);
        if (obj.GetComponent<Cebolla>() != null) Add<Cebolla>(obj);
        if (obj.GetComponent<Sillao>() != null) Add<Sillao>(obj);

        CheckReceta();
    }

    void Add<T>(GameObject obj)
    {
        if (ingredientes.Contains(typeof(T)))
            return;

        ingredientes.Add(typeof(T));
        ingredientesUsados.Add(obj); //  guardar referencia

        obj.transform.SetParent(transform, false);
        obj.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        //  bloquear interacción
        CanvasGroup cg = obj.GetComponent<CanvasGroup>();
        if (cg != null)
        {
            cg.blocksRaycasts = false;
            cg.interactable = false;
        }

        var drag = obj.GetComponent<UIDragDrop>();
        if (drag != null) drag.enabled = false;
    }

    void CheckReceta()
    {
        bool completa =
            ingredientes.Contains(typeof(Arroz)) &&
            ingredientes.Contains(typeof(PolloTrozos)) &&
            ingredientes.Contains(typeof(Huevo)) &&
            ingredientes.Contains(typeof(Cebolla)) &&
            ingredientes.Contains(typeof(Sillao));

        if (completa)
        {
            listoParaCocinar = true;
            Debug.Log("RECETA COMPLETA");
        }
    }

    //  RESET DE ESTA ESTACIÓN
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

            //  restaurar posición UI
            RectTransform rect = obj.GetComponent<RectTransform>();
            rect.anchoredPosition = init.initialPosition;

            //  asegurar que se vea (no quede detrás)
            obj.transform.SetAsLastSibling();

            //  reactivar interacción
            CanvasGroup cg = obj.GetComponent<CanvasGroup>();
            if (cg != null)
            {
                cg.blocksRaycasts = true;
                cg.interactable = true;
                cg.alpha = 1f;
            }

            //  reactivar drag
            var drag = obj.GetComponent<UIDragDrop>();
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