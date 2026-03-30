using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class UIScreenNavigator : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private RectTransform container;

    [Header("Panels")]
    [SerializeField] private RectTransform panelCentro;
    [SerializeField] private RectTransform panelIzquierda;
    [SerializeField] private RectTransform panelDerecha;
    [SerializeField] private RectTransform panelAbajo;

    [Header("Config")]
    [SerializeField] private float moveDuration = 0.3f;

    private Vector2 screenSize;
    private Vector2 targetPosition;
    private bool isMoving;

    // Estado actual (clave)
    private Vector2Int currentScreen = Vector2Int.zero;

    private void Start()
    {
        RectTransform parentRect = container.parent as RectTransform;
        screenSize = parentRect.rect.size;

        Debug.Log($"[Navigator] ScreenSize UI: {screenSize}");

        SetupPanels();

        targetPosition = container.anchoredPosition;
    }

    private void SetupPanels()
    {
        panelCentro.anchoredPosition = Vector2.zero;
        panelIzquierda.anchoredPosition = new Vector2(-screenSize.x, 0);
        panelDerecha.anchoredPosition = new Vector2(screenSize.x, 0);
        panelAbajo.anchoredPosition = new Vector2(0, -screenSize.y);

        Debug.Log("[Navigator] Panels positioned correctly");
    }

    private void Update()
    {
        if (isMoving) return;

        var kb = Keyboard.current;
        if (kb == null) return;

        if (kb.rightArrowKey.wasPressedThisFrame)
            TryMove(new Vector2Int(1, 0));

        if (kb.leftArrowKey.wasPressedThisFrame)
            TryMove(new Vector2Int(-1, 0));

        if (kb.downArrowKey.wasPressedThisFrame)
            TryMove(new Vector2Int(0, -1));

        if (kb.upArrowKey.wasPressedThisFrame)
            TryMove(new Vector2Int(0, 1));
    }

    void TryMove(Vector2Int dir)
    {
        Vector2Int next = currentScreen + dir;

        if (!IsValid(next))
        {
            Debug.Log($"[Navigator] Movimiento inv�lido: {next}");
            return;
        }

        currentScreen = next;

        AudioManager.Instance.Play("Slide");

        Vector2 newPos = new Vector2(
            -currentScreen.x * screenSize.x,
            -currentScreen.y * screenSize.y
        );

        MoveTo(newPos);

        Debug.Log($"[Navigator] Screen actual: {currentScreen}");
    }

    bool IsValid(Vector2Int pos)
    {
        if (pos == Vector2Int.zero) return true;                // centro
        if (pos == new Vector2Int(-1, 0)) return true;          // izquierda
        if (pos == new Vector2Int(1, 0)) return true;           // derecha
        if (pos == new Vector2Int(0, -1)) return true;          // abajo

        return false;
    }

    void MoveTo(Vector2 newPos)
    {
        targetPosition = newPos;
        StartCoroutine(SmoothMove());
    }

    IEnumerator SmoothMove()
    {
        isMoving = true;

        Vector2 start = container.anchoredPosition;
        float time = 0f;

        while (time < moveDuration)
        {
            time += Time.deltaTime;
            float t = time / moveDuration;

            // SmoothStep
            t = t * t * (3f - 2f * t);

            container.anchoredPosition = Vector2.Lerp(start, targetPosition, t);

            yield return null;
        }

        container.anchoredPosition = targetPosition;
        isMoving = false;
    }
}