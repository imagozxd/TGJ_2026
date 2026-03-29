using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

// Componente que va en el GameObject del plato (junto con PlatoData y PlatoClickable).
// Gestiona la bebida + cremas de ese slot y crea los visuales como hijos del plato.
public class SlotMostrador : MonoBehaviour
{
    [Header("Offsets de visuales (en pixeles, relativo al centro del plato)")]
    [SerializeField] public Vector2 offsetBebida = new Vector2(100f, 0f);
    [SerializeField] public Vector2 offsetPrimeraCrema = new Vector2(-100f, 0f);
    [SerializeField] public float espaciadoCremaY = -55f;

    // referencia al PlatoData del mismo GameObject
    public PlatoData platoData;

    // datos de bebida
    public bool tieneBebida;
    public Bebida bebida;
    public TamañoBebida tamañoBebida;

    // datos de cremas
    public List<Crema> cremas = new List<Crema>();

    // visuales creados (hijos de este GameObject)
    private List<GameObject> visuales = new List<GameObject>();

    // ── Bebida ───────────────────────────────────────────────────
    public void AgregarBebida(BebidaItem item)
    {
        // reemplazar visual anterior si ya había bebida
        if (tieneBebida)
        {
            var anterior = visuales.Find(v => v != null && v.name == "VisualBebida");
            if (anterior != null) { visuales.Remove(anterior); Destroy(anterior); }
        }

        tieneBebida  = true;
        bebida       = item.Tipo;
        tamañoBebida = item.Tamaño;

        CrearVisual("VisualBebida", item.Sprite, item.Escala, offsetBebida);
    }

    // ── Crema ────────────────────────────────────────────────────
    public void AgregarCrema(CremaItem item)
    {
        if (cremas.Contains(item.Tipo))
        {
            Debug.Log("Crema ya añadida: " + item.Tipo);
            return;
        }

        int idx = cremas.Count;   // índice antes de añadir
        cremas.Add(item.Tipo);

        Vector2 offset = offsetPrimeraCrema + new Vector2(0f, espaciadoCremaY * idx);
        CrearVisual("VisualCrema_" + item.Tipo, item.Sprite, item.Escala, offset);
    }

    // ── Visual interno ───────────────────────────────────────────
    void CrearVisual(string nombre, Sprite sprite, Vector3 escala, Vector2 offset)
    {
        if (sprite == null) return;

        GameObject go = new GameObject(nombre, typeof(RectTransform), typeof(Image));
        go.transform.SetParent(transform, false);

        RectTransform rect = go.GetComponent<RectTransform>();
        rect.anchoredPosition = offset;
        rect.sizeDelta = new Vector2(80f, 80f);
        go.transform.localScale = escala;

        Image img = go.GetComponent<Image>();
        img.sprite  = sprite;
        img.preserveAspect = true;

        visuales.Add(go);
    }

    // ── Limpieza ─────────────────────────────────────────────────
    public void LimpiarVisuales()
    {
        foreach (var v in visuales)
            if (v != null) Destroy(v);
        visuales.Clear();

        tieneBebida  = false;
        bebida       = default;
        tamañoBebida = default;
        cremas.Clear();
    }

    public bool TieneContenido() => platoData != null;
}
